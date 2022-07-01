using Microsoft.CognitiveServices.Speech;
using NAudio.Lame;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TTSTool
{
    public partial class Mainform : Form
    {
        Config config;
        bool loaded = false;
        public Mainform()
        {
            var langs = GetLanguages().GroupBy(t => new { t.lang, t.code }).Select(t => t.Key).ToArray();
            InitializeComponent();
            config = Config.LoadConfig();
            txtOutputFolder.Text = config.OutputFolder;

            cbLanguage.DisplayMember = "lang";
            cbLanguage.ValueMember = "code";
            cbLanguage.DataSource = langs;
            //cbLanguage.SelectedItem = langs.First(t => t.code == "zh-CN");

            if (config.Lang != null)
            {
                cbLanguage.SelectedValue = config.Lang;
                cbVoice.SelectedValue = config.Voice;
                cbSpeakingStyle.SelectedItem = config.Style;
                cbRolePlay.SelectedItem = config.Role;

                numPitch.Value = config.Pitch;
                numSpeakingSpeed.Value = config.Speed;

                chkMP3.Checked = config.MP3;
                chkKeepWav.Checked = config.KeepWAV;
                chkStero.Checked = config.Stero;
            }
            loaded = true;
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            var settingDialog = new SettingDialog(config);
            if (settingDialog.ShowDialog() == DialogResult.OK)
            {
                config = settingDialog.Config;
                config.SaveConfig();
            }
        }

        private async void btnGO_Click(object sender, EventArgs e)
        {
            btnGO.Enabled = false;
            await TTS();
            btnGO.Enabled = true;
        }

        private void cbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            var voices = GetLanguages().Where(t => t.code == cbLanguage.SelectedValue.ToString()).Select(t => new { t.voice, t.gender }).ToArray();
            cbVoice.DisplayMember = "voice";
            cbVoice.ValueMember = "voice";
            cbVoice.DataSource = voices;
            BuildSSMLAndSaveLatestConfig();
        }

        private void cbVoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            var style = GetVoiceStyles().Where(t => t.voice == cbVoice.SelectedValue.ToString()).FirstOrDefault();
            var list = new List<string> { "general" };

            if (style.voice != null)
            {
                list.AddRange(style.styles);
            }
            cbSpeakingStyle.DataSource = list;
            BuildSSMLAndSaveLatestConfig();
        }

        private void RebuildSSML(object sender, EventArgs e) => BuildSSMLAndSaveLatestConfig();

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            BuildSSMLAndSaveLatestConfig();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = txtOutputFolder.Text
            });
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtOutputFolder.Text = folderBrowserDialog1.SelectedPath;
                config.OutputFolder = folderBrowserDialog1.SelectedPath;
                config.SaveConfig();
            }
        }

        private void chkMP3_CheckedChanged(object sender, EventArgs e)
        {
            chkKeepWav.Enabled = chkMP3.Checked;
        }

        private (string lang, string code, string gender, string voice, string type)[] GetLanguages()
        {
            #region plain text from https://docs.microsoft.com/en-us/azure/cognitive-services/speech-service/language-support?tabs=plaintext
            var text = @"Afrikaans (South Africa)	af-ZA	Female	af-ZA-AdriNeural	General
Afrikaans (South Africa)	af-ZA	Male	af-ZA-WillemNeural	General
Albanian (Albania)	sq-AL	Female	sq-AL-AnilaNeural New	General
Albanian (Albania)	sq-AL	Male	sq-AL-IlirNeural New	General
Amharic (Ethiopia)	am-ET	Female	am-ET-MekdesNeural	General
Amharic (Ethiopia)	am-ET	Male	am-ET-AmehaNeural	General
Arabic (Algeria)	ar-DZ	Female	ar-DZ-AminaNeural	General
Arabic (Algeria)	ar-DZ	Male	ar-DZ-IsmaelNeural	General
Arabic (Bahrain)	ar-BH	Female	ar-BH-LailaNeural	General
Arabic (Bahrain)	ar-BH	Male	ar-BH-AliNeural	General
Arabic (Egypt)	ar-EG	Female	ar-EG-SalmaNeural	General
Arabic (Egypt)	ar-EG	Male	ar-EG-ShakirNeural	General
Arabic (Iraq)	ar-IQ	Female	ar-IQ-RanaNeural	General
Arabic (Iraq)	ar-IQ	Male	ar-IQ-BasselNeural	General
Arabic (Jordan)	ar-JO	Female	ar-JO-SanaNeural	General
Arabic (Jordan)	ar-JO	Male	ar-JO-TaimNeural	General
Arabic (Kuwait)	ar-KW	Female	ar-KW-NouraNeural	General
Arabic (Kuwait)	ar-KW	Male	ar-KW-FahedNeural	General
Arabic (Lebanon)	ar-LB	Female	ar-LB-LaylaNeural New	General
Arabic (Lebanon)	ar-LB	Male	ar-LB-RamiNeural New	General
Arabic (Libya)	ar-LY	Female	ar-LY-ImanNeural	General
Arabic (Libya)	ar-LY	Male	ar-LY-OmarNeural	General
Arabic (Morocco)	ar-MA	Female	ar-MA-MounaNeural	General
Arabic (Morocco)	ar-MA	Male	ar-MA-JamalNeural	General
Arabic (Oman)	ar-OM	Female	ar-OM-AyshaNeural New	General
Arabic (Oman)	ar-OM	Male	ar-OM-AbdullahNeural New	General
Arabic (Qatar)	ar-QA	Female	ar-QA-AmalNeural	General
Arabic (Qatar)	ar-QA	Male	ar-QA-MoazNeural	General
Arabic (Saudi Arabia)	ar-SA	Female	ar-SA-ZariyahNeural	General
Arabic (Saudi Arabia)	ar-SA	Male	ar-SA-HamedNeural	General
Arabic (Syria)	ar-SY	Female	ar-SY-AmanyNeural	General
Arabic (Syria)	ar-SY	Male	ar-SY-LaithNeural	General
Arabic (Tunisia)	ar-TN	Female	ar-TN-ReemNeural	General
Arabic (Tunisia)	ar-TN	Male	ar-TN-HediNeural	General
Arabic (United Arab Emirates)	ar-AE	Female	ar-AE-FatimaNeural	General
Arabic (United Arab Emirates)	ar-AE	Male	ar-AE-HamdanNeural	General
Arabic (Yemen)	ar-YE	Female	ar-YE-MaryamNeural	General
Arabic (Yemen)	ar-YE	Male	ar-YE-SalehNeural	General
Azerbaijani (Azerbaijan)	az-AZ	Female	az-AZ-BabekNeural New	General
Azerbaijani (Azerbaijan)	az-AZ	Male	az-AZ-BanuNeural New	General
Bangla (Bangladesh)	bn-BD	Female	bn-BD-NabanitaNeural	General
Bangla (Bangladesh)	bn-BD	Male	bn-BD-PradeepNeural	General
Bengali (India)	bn-IN	Female	bn-IN-TanishaaNeural	General
Bengali (India)	bn-IN	Male	bn-IN-BashkarNeural	General
Bosnian (Bosnia and Herzegovina)	bs-BA	Female	bs-BA-VesnaNeural New	General
Bosnian (Bosnia and Herzegovina)	bs-BA	Male	bs-BA-GoranNeural New	General
Bulgarian (Bulgaria)	bg-BG	Female	bg-BG-KalinaNeural	General
Bulgarian (Bulgaria)	bg-BG	Male	bg-BG-BorislavNeural	General
Burmese (Myanmar)	my-MM	Female	my-MM-NilarNeural	General
Burmese (Myanmar)	my-MM	Male	my-MM-ThihaNeural	General
Catalan (Spain)	ca-ES	Female	ca-ES-AlbaNeural	General
Catalan (Spain)	ca-ES	Female	ca-ES-JoanaNeural	General
Catalan (Spain)	ca-ES	Male	ca-ES-EnricNeural	General
Chinese (Cantonese, Traditional)	zh-HK	Female	zh-HK-HiuGaaiNeural	General
Chinese (Cantonese, Traditional)	zh-HK	Female	zh-HK-HiuMaanNeural	General
Chinese (Cantonese, Traditional)	zh-HK	Male	zh-HK-WanLungNeural	General
Chinese (Mandarin, Simplified)	zh-CN	Female	zh-CN-XiaochenNeural	Optimized for spontaneous conversation
Chinese (Mandarin, Simplified)	zh-CN	Female	zh-CN-XiaohanNeural	General, multiple styles available using SSML
Chinese (Mandarin, Simplified)	zh-CN	Female	zh-CN-XiaomoNeural	General, multiple role-play and styles available using SSML
Chinese (Mandarin, Simplified)	zh-CN	Female	zh-CN-XiaoqiuNeural	Optimized for narrating
Chinese (Mandarin, Simplified)	zh-CN	Female	zh-CN-XiaoruiNeural	Senior voice, multiple styles available using SSML
Chinese (Mandarin, Simplified)	zh-CN	Female	zh-CN-XiaoshuangNeural	Child voice, optimized for child story and chat; multiple voice styles available using SSML
Chinese (Mandarin, Simplified)	zh-CN	Female	zh-CN-XiaoxiaoNeural	General, multiple voice styles available using SSML
Chinese (Mandarin, Simplified)	zh-CN	Female	zh-CN-XiaoxuanNeural	General, multiple role-play and styles available using SSML
Chinese (Mandarin, Simplified)	zh-CN	Female	zh-CN-XiaoyanNeural	Optimized for customer service
Chinese (Mandarin, Simplified)	zh-CN	Female	zh-CN-XiaoyouNeural	Child voice, optimized for story narrating
Chinese (Mandarin, Simplified)	zh-CN	Male	zh-CN-YunxiNeural	General, multiple styles available using SSML
Chinese (Mandarin, Simplified)	zh-CN	Male	zh-CN-YunyangNeural	Optimized for news reading,
multiple voice styles available using SSML
Chinese (Mandarin, Simplified)	zh-CN	Male	zh-CN-YunyeNeural	Optimized for story narrating, multiple role-play and styles available using SSML
Chinese (Taiwanese Mandarin)	zh-TW	Female	zh-TW-HsiaoChenNeural	General
Chinese (Taiwanese Mandarin)	zh-TW	Female	zh-TW-HsiaoYuNeural	General
Chinese (Taiwanese Mandarin)	zh-TW	Male	zh-TW-YunJheNeural	General
Croatian (Croatia)	hr-HR	Female	hr-HR-GabrijelaNeural	General
Croatian (Croatia)	hr-HR	Male	hr-HR-SreckoNeural	General
Czech (Czech)	cs-CZ	Female	cs-CZ-VlastaNeural	General
Czech (Czech)	cs-CZ	Male	cs-CZ-AntoninNeural	General
Danish (Denmark)	da-DK	Female	da-DK-ChristelNeural	General
Danish (Denmark)	da-DK	Male	da-DK-JeppeNeural	General
Dutch (Belgium)	nl-BE	Female	nl-BE-DenaNeural	General
Dutch (Belgium)	nl-BE	Male	nl-BE-ArnaudNeural	General
Dutch (Netherlands)	nl-NL	Female	nl-NL-ColetteNeural	General
Dutch (Netherlands)	nl-NL	Female	nl-NL-FennaNeural	General
Dutch (Netherlands)	nl-NL	Male	nl-NL-MaartenNeural	General
English (Australia)	en-AU	Female	en-AU-NatashaNeural	General
English (Australia)	en-AU	Male	en-AU-WilliamNeural	General
English (Canada)	en-CA	Female	en-CA-ClaraNeural	General
English (Canada)	en-CA	Male	en-CA-LiamNeural	General
English (Hongkong)	en-HK	Female	en-HK-YanNeural	General
English (Hongkong)	en-HK	Male	en-HK-SamNeural	General
English (India)	en-IN	Female	en-IN-NeerjaNeural	General
English (India)	en-IN	Male	en-IN-PrabhatNeural	General
English (Ireland)	en-IE	Female	en-IE-EmilyNeural	General
English (Ireland)	en-IE	Male	en-IE-ConnorNeural	General
English (Kenya)	en-KE	Female	en-KE-AsiliaNeural	General
English (Kenya)	en-KE	Male	en-KE-ChilembaNeural	General
English (New Zealand)	en-NZ	Female	en-NZ-MollyNeural	General
English (New Zealand)	en-NZ	Male	en-NZ-MitchellNeural	General
English (Nigeria)	en-NG	Female	en-NG-EzinneNeural	General
English (Nigeria)	en-NG	Male	en-NG-AbeoNeural	General
English (Philippines)	en-PH	Female	en-PH-RosaNeural	General
English (Philippines)	en-PH	Male	en-PH-JamesNeural	General
English (Singapore)	en-SG	Female	en-SG-LunaNeural	General
English (Singapore)	en-SG	Male	en-SG-WayneNeural	General
English (South Africa)	en-ZA	Female	en-ZA-LeahNeural	General
English (South Africa)	en-ZA	Male	en-ZA-LukeNeural	General
English (Tanzania)	en-TZ	Female	en-TZ-ImaniNeural	General
English (Tanzania)	en-TZ	Male	en-TZ-ElimuNeural	General
English (United Kingdom)	en-GB	Female	en-GB-AbbiNeural	General
English (United Kingdom)	en-GB	Female	en-GB-BellaNeural	General
English (United Kingdom)	en-GB	Female	en-GB-HollieNeural	General
English (United Kingdom)	en-GB	Female	en-GB-LibbyNeural	General
English (United Kingdom)	en-GB	Female	en-GB-MaisieNeural	General, child voice
English (United Kingdom)	en-GB	Female	en-GB-MiaNeural Retired on 30 October 2021, see below	General
English (United Kingdom)	en-GB	Female	en-GB-OliviaNeural	General
English (United Kingdom)	en-GB	Female	en-GB-SoniaNeural	General
English (United Kingdom)	en-GB	Male	en-GB-AlfieNeural	General
English (United Kingdom)	en-GB	Male	en-GB-ElliotNeural	General
English (United Kingdom)	en-GB	Male	en-GB-EthanNeural	General
English (United Kingdom)	en-GB	Male	en-GB-NoahNeural	General
English (United Kingdom)	en-GB	Male	en-GB-OliverNeural	General
English (United Kingdom)	en-GB	Male	en-GB-RyanNeural	General
English (United Kingdom)	en-GB	Male	en-GB-ThomasNeural	General
English (United States)	en-US	Female	en-US-AmberNeural	General
English (United States)	en-US	Female	en-US-AriaNeural	General, multiple voice styles available using SSML
English (United States)	en-US	Female	en-US-AshleyNeural	General
English (United States)	en-US	Female	en-US-CoraNeural	General
English (United States)	en-US	Female	en-US-ElizabethNeural	General
English (United States)	en-US	Female	en-US-JennyNeural	General, multiple voice styles available using SSML
English (United States)	en-US as the primary default. Additional locales are supported using SSML	Female	en-US-JennyMultilingualNeural	General
English (United States)	en-US	Female	en-US-MichelleNeural	General
English (United States)	en-US	Female	en-US-MonicaNeural	General
English (United States)	en-US	Female	en-US-SaraNeural	General, multiple voice styles available using SSML
English (United States)	en-US	Kid	en-US-AnaNeural	General
English (United States)	en-US	Male	en-US-BrandonNeural	General
English (United States)	en-US	Male	en-US-ChristopherNeural	General
English (United States)	en-US	Male	en-US-EricNeural	General
English (United States)	en-US	Male	en-US-GuyNeural	General, multiple voice styles available using SSML
English (United States)	en-US	Male	en-US-JacobNeural	General
Estonian (Estonia)	et-EE	Female	et-EE-AnuNeural	General
Estonian (Estonia)	et-EE	Male	et-EE-KertNeural	General
Filipino (Philippines)	fil-PH	Female	fil-PH-BlessicaNeural	General
Filipino (Philippines)	fil-PH	Male	fil-PH-AngeloNeural	General
Finnish (Finland)	fi-FI	Female	fi-FI-NooraNeural	General
Finnish (Finland)	fi-FI	Female	fi-FI-SelmaNeural	General
Finnish (Finland)	fi-FI	Male	fi-FI-HarriNeural	General
French (Belgium)	fr-BE	Female	fr-BE-CharlineNeural	General
French (Belgium)	fr-BE	Male	fr-BE-GerardNeural	General
French (Canada)	fr-CA	Female	fr-CA-SylvieNeural	General
French (Canada)	fr-CA	Male	fr-CA-AntoineNeural	General
French (Canada)	fr-CA	Male	fr-CA-JeanNeural	General
French (France)	fr-FR	Female	fr-FR-BrigitteNeural	General
French (France)	fr-FR	Female	fr-FR-CelesteNeural	General
French (France)	fr-FR	Female	fr-FR-CoralieNeural	General
French (France)	fr-FR	Female	fr-FR-DeniseNeural	General, multiple voice styles available using SSML Public preview
French (France)	fr-FR	Female	fr-FR-EloiseNeural	General, child voice
French (France)	fr-FR	Female	fr-FR-JacquelineNeural	General
French (France)	fr-FR	Female	fr-FR-JosephineNeural	General
French (France)	fr-FR	Female	fr-FR-YvetteNeural	General
French (France)	fr-FR	Male	fr-FR-AlainNeural	General
French (France)	fr-FR	Male	fr-FR-ClaudeNeural	General
French (France)	fr-FR	Male	fr-FR-HenriNeural	General
French (France)	fr-FR	Male	fr-FR-JeromeNeural	General
French (France)	fr-FR	Male	fr-FR-MauriceNeural	General
French (France)	fr-FR	Male	fr-FR-YvesNeural	General
French (Switzerland)	fr-CH	Female	fr-CH-ArianeNeural	General
French (Switzerland)	fr-CH	Male	fr-CH-FabriceNeural	General
Galician (Spain)	gl-ES	Female	gl-ES-SabelaNeural	General
Galician (Spain)	gl-ES	Male	gl-ES-RoiNeural	General
Georgian (Georgia)	ka-GE	Female	ka-GE-EkaNeural New	General
Georgian (Georgia)	ka-GE	Male	ka-GE-GiorgiNeural New	General
German (Austria)	de-AT	Female	de-AT-IngridNeural	General
German (Austria)	de-AT	Male	de-AT-JonasNeural	General
German (Germany)	de-DE	Female	de-DE-AmalaNeural	General
German (Germany)	de-DE	Female	de-DE-ElkeNeural	General
German (Germany)	de-DE	Female	de-DE-GiselaNeural	General, child voice
German (Germany)	de-DE	Female	de-DE-KatjaNeural	General
German (Germany)	de-DE	Female	de-DE-KlarissaNeural	General
German (Germany)	de-DE	Female	de-DE-LouisaNeural	General
German (Germany)	de-DE	Female	de-DE-MajaNeural	General
German (Germany)	de-DE	Female	de-DE-TanjaNeural	General
German (Germany)	de-DE	Male	de-DE-BerndNeural	General
German (Germany)	de-DE	Male	de-DE-ChristophNeural	General
German (Germany)	de-DE	Male	de-DE-ConradNeural	General
German (Germany)	de-DE	Male	de-DE-KasperNeural	General
German (Germany)	de-DE	Male	de-DE-KillianNeural	General
German (Germany)	de-DE	Male	de-DE-KlausNeural	General
German (Germany)	de-DE	Male	de-DE-RalfNeural	General
German (Switzerland)	de-CH	Female	de-CH-LeniNeural	General
German (Switzerland)	de-CH	Male	de-CH-JanNeural	General
Greek (Greece)	el-GR	Female	el-GR-AthinaNeural	General
Greek (Greece)	el-GR	Male	el-GR-NestorasNeural	General
Gujarati (India)	gu-IN	Female	gu-IN-DhwaniNeural	General
Gujarati (India)	gu-IN	Male	gu-IN-NiranjanNeural	General
Hebrew (Israel)	he-IL	Female	he-IL-HilaNeural	General
Hebrew (Israel)	he-IL	Male	he-IL-AvriNeural	General
Hindi (India)	hi-IN	Female	hi-IN-SwaraNeural	General
Hindi (India)	hi-IN	Male	hi-IN-MadhurNeural	General
Hungarian (Hungary)	hu-HU	Female	hu-HU-NoemiNeural	General
Hungarian (Hungary)	hu-HU	Male	hu-HU-TamasNeural	General
Icelandic (Iceland)	is-IS	Female	is-IS-GudrunNeural	General
Icelandic (Iceland)	is-IS	Male	is-IS-GunnarNeural	General
Indonesian (Indonesia)	id-ID	Female	id-ID-GadisNeural	General
Indonesian (Indonesia)	id-ID	Male	id-ID-ArdiNeural	General
Irish (Ireland)	ga-IE	Female	ga-IE-OrlaNeural	General
Irish (Ireland)	ga-IE	Male	ga-IE-ColmNeural	General
Italian (Italy)	it-IT	Female	it-IT-ElsaNeural	General
Italian (Italy)	it-IT	Female	it-IT-IsabellaNeural	General
Italian (Italy)	it-IT	Male	it-IT-DiegoNeural	General
Japanese (Japan)	ja-JP	Female	ja-JP-NanamiNeural	General
Japanese (Japan)	ja-JP	Male	ja-JP-KeitaNeural	General
Javanese (Indonesia)	jv-ID	Female	jv-ID-SitiNeural	General
Javanese (Indonesia)	jv-ID	Male	jv-ID-DimasNeural	General
Kannada (India)	kn-IN	Female	kn-IN-SapnaNeural	General
Kannada (India)	kn-IN	Male	kn-IN-GaganNeural	General
Kazakh (Kazakhstan)	kk-KZ	Female	kk-KZ-AigulNeural	General
Kazakh (Kazakhstan)	kk-KZ	Male	kk-KZ-DauletNeural	General
Khmer (Cambodia)	km-KH	Female	km-KH-SreymomNeural	General
Khmer (Cambodia)	km-KH	Male	km-KH-PisethNeural	General
Korean (Korea)	ko-KR	Female	ko-KR-SunHiNeural	General
Korean (Korea)	ko-KR	Male	ko-KR-InJoonNeural	General
Lao (Laos)	lo-LA	Female	lo-LA-KeomanyNeural	General
Lao (Laos)	lo-LA	Male	lo-LA-ChanthavongNeural	General
Latvian (Latvia)	lv-LV	Female	lv-LV-EveritaNeural	General
Latvian (Latvia)	lv-LV	Male	lv-LV-NilsNeural	General
Lithuanian (Lithuania)	lt-LT	Female	lt-LT-OnaNeural	General
Lithuanian (Lithuania)	lt-LT	Male	lt-LT-LeonasNeural	General
Macedonian (Republic of North Macedonia)	mk-MK	Female	mk-MK-MarijaNeural	General
Macedonian (Republic of North Macedonia)	mk-MK	Male	mk-MK-AleksandarNeural	General
Malay (Malaysia)	ms-MY	Female	ms-MY-YasminNeural	General
Malay (Malaysia)	ms-MY	Male	ms-MY-OsmanNeural	General
Malayalam (India)	ml-IN	Female	ml-IN-SobhanaNeural	General
Malayalam (India)	ml-IN	Male	ml-IN-MidhunNeural	General
Maltese (Malta)	mt-MT	Female	mt-MT-GraceNeural	General
Maltese (Malta)	mt-MT	Male	mt-MT-JosephNeural	General
Marathi (India)	mr-IN	Female	mr-IN-AarohiNeural	General
Marathi (India)	mr-IN	Male	mr-IN-ManoharNeural	General
Mongolian (Mongolia)	mn-MN	Female	mn-MN-YesuiNeural New	General
Mongolian (Mongolia)	mn-MN	Male	mn-MN-BataaNeural New	General
Nepali (Nepal)	ne-NP	Female	ne-NP-HemkalaNeural New	General
Nepali (Nepal)	ne-NP	Male	ne-NP-SagarNeural New	General
Norwegian (Bokmål, Norway)	nb-NO	Female	nb-NO-IselinNeural	General
Norwegian (Bokmål, Norway)	nb-NO	Female	nb-NO-PernilleNeural	General
Norwegian (Bokmål, Norway)	nb-NO	Male	nb-NO-FinnNeural	General
Pashto (Afghanistan)	ps-AF	Female	ps-AF-LatifaNeural	General
Pashto (Afghanistan)	ps-AF	Male	ps-AF-GulNawazNeural	General
Persian (Iran)	fa-IR	Female	fa-IR-DilaraNeural	General
Persian (Iran)	fa-IR	Male	fa-IR-FaridNeural	General
Polish (Poland)	pl-PL	Female	pl-PL-AgnieszkaNeural	General
Polish (Poland)	pl-PL	Female	pl-PL-ZofiaNeural	General
Polish (Poland)	pl-PL	Male	pl-PL-MarekNeural	General
Portuguese (Brazil)	pt-BR	Female	pt-BR-FranciscaNeural	General, multiple voice styles available using SSML
Portuguese (Brazil)	pt-BR	Male	pt-BR-AntonioNeural	General
Portuguese (Portugal)	pt-PT	Female	pt-PT-FernandaNeural	General
Portuguese (Portugal)	pt-PT	Female	pt-PT-RaquelNeural	General
Portuguese (Portugal)	pt-PT	Male	pt-PT-DuarteNeural	General
Romanian (Romania)	ro-RO	Female	ro-RO-AlinaNeural	General
Romanian (Romania)	ro-RO	Male	ro-RO-EmilNeural	General
Russian (Russia)	ru-RU	Female	ru-RU-DariyaNeural	General
Russian (Russia)	ru-RU	Female	ru-RU-SvetlanaNeural	General
Russian (Russia)	ru-RU	Male	ru-RU-DmitryNeural	General
Serbian (Serbia, Cyrillic)	sr-RS	Female	sr-RS-SophieNeural	General
Serbian (Serbia, Cyrillic)	sr-RS	Male	sr-RS-NicholasNeural	General
Sinhala (Sri Lanka)	si-LK	Female	si-LK-ThiliniNeural	General
Sinhala (Sri Lanka)	si-LK	Male	si-LK-SameeraNeural	General
Slovak (Slovakia)	sk-SK	Female	sk-SK-ViktoriaNeural	General
Slovak (Slovakia)	sk-SK	Male	sk-SK-LukasNeural	General
Slovenian (Slovenia)	sl-SI	Female	sl-SI-PetraNeural	General
Slovenian (Slovenia)	sl-SI	Male	sl-SI-RokNeural	General
Somali (Somalia)	so-SO	Female	so-SO-UbaxNeural	General
Somali (Somalia)	so-SO	Male	so-SO-MuuseNeural	General
Spanish (Argentina)	es-AR	Female	es-AR-ElenaNeural	General
Spanish (Argentina)	es-AR	Male	es-AR-TomasNeural	General
Spanish (Bolivia)	es-BO	Female	es-BO-SofiaNeural	General
Spanish (Bolivia)	es-BO	Male	es-BO-MarceloNeural	General
Spanish (Chile)	es-CL	Female	es-CL-CatalinaNeural	General
Spanish (Chile)	es-CL	Male	es-CL-LorenzoNeural	General
Spanish (Colombia)	es-CO	Female	es-CO-SalomeNeural	General
Spanish (Colombia)	es-CO	Male	es-CO-GonzaloNeural	General
Spanish (Costa Rica)	es-CR	Female	es-CR-MariaNeural	General
Spanish (Costa Rica)	es-CR	Male	es-CR-JuanNeural	General
Spanish (Cuba)	es-CU	Female	es-CU-BelkysNeural	General
Spanish (Cuba)	es-CU	Male	es-CU-ManuelNeural	General
Spanish (Dominican Republic)	es-DO	Female	es-DO-RamonaNeural	General
Spanish (Dominican Republic)	es-DO	Male	es-DO-EmilioNeural	General
Spanish (Ecuador)	es-EC	Female	es-EC-AndreaNeural	General
Spanish (Ecuador)	es-EC	Male	es-EC-LuisNeural	General
Spanish (El Salvador)	es-SV	Female	es-SV-LorenaNeural	General
Spanish (El Salvador)	es-SV	Male	es-SV-RodrigoNeural	General
Spanish (Equatorial Guinea)	es-GQ	Female	es-GQ-TeresaNeural	General
Spanish (Equatorial Guinea)	es-GQ	Male	es-GQ-JavierNeural	General
Spanish (Guatemala)	es-GT	Female	es-GT-MartaNeural	General
Spanish (Guatemala)	es-GT	Male	es-GT-AndresNeural	General
Spanish (Honduras)	es-HN	Female	es-HN-KarlaNeural	General
Spanish (Honduras)	es-HN	Male	es-HN-CarlosNeural	General
Spanish (Mexico)	es-MX	Female	es-MX-DaliaNeural	General
Spanish (Mexico)	es-MX	Male	es-MX-JorgeNeural	General
Spanish (Nicaragua)	es-NI	Female	es-NI-YolandaNeural	General
Spanish (Nicaragua)	es-NI	Male	es-NI-FedericoNeural	General
Spanish (Panama)	es-PA	Female	es-PA-MargaritaNeural	General
Spanish (Panama)	es-PA	Male	es-PA-RobertoNeural	General
Spanish (Paraguay)	es-PY	Female	es-PY-TaniaNeural	General
Spanish (Paraguay)	es-PY	Male	es-PY-MarioNeural	General
Spanish (Peru)	es-PE	Female	es-PE-CamilaNeural	General
Spanish (Peru)	es-PE	Male	es-PE-AlexNeural	General
Spanish (Puerto Rico)	es-PR	Female	es-PR-KarinaNeural	General
Spanish (Puerto Rico)	es-PR	Male	es-PR-VictorNeural	General
Spanish (Spain)	es-ES	Female	es-ES-ElviraNeural	General
Spanish (Spain)	es-ES	Male	es-ES-AlvaroNeural	General
Spanish (Uruguay)	es-UY	Female	es-UY-ValentinaNeural	General
Spanish (Uruguay)	es-UY	Male	es-UY-MateoNeural	General
Spanish (US)	es-US	Female	es-US-PalomaNeural	General
Spanish (US)	es-US	Male	es-US-AlonsoNeural	General
Spanish (Venezuela)	es-VE	Female	es-VE-PaolaNeural	General
Spanish (Venezuela)	es-VE	Male	es-VE-SebastianNeural	General
Sundanese (Indonesia)	su-ID	Female	su-ID-TutiNeural	General
Sundanese (Indonesia)	su-ID	Male	su-ID-JajangNeural	General
Swahili (Kenya)	sw-KE	Female	sw-KE-ZuriNeural	General
Swahili (Kenya)	sw-KE	Male	sw-KE-RafikiNeural	General
Swahili (Tanzania)	sw-TZ	Female	sw-TZ-RehemaNeural	General
Swahili (Tanzania)	sw-TZ	Male	sw-TZ-DaudiNeural	General
Swedish (Sweden)	sv-SE	Female	sv-SE-HilleviNeural	General
Swedish (Sweden)	sv-SE	Female	sv-SE-SofieNeural	General
Swedish (Sweden)	sv-SE	Male	sv-SE-MattiasNeural	General
Tamil (India)	ta-IN	Female	ta-IN-PallaviNeural	General
Tamil (India)	ta-IN	Male	ta-IN-ValluvarNeural	General
Tamil (Malaysia)	ta-MY	Female	ta-MY-KaniNeural New	General
Tamil (Malaysia)	ta-MY	Male	ta-MY-SuryaNeural New	General
Tamil (Singapore)	ta-SG	Female	ta-SG-VenbaNeural	General
Tamil (Singapore)	ta-SG	Male	ta-SG-AnbuNeural	General
Tamil (Sri Lanka)	ta-LK	Female	ta-LK-SaranyaNeural	General
Tamil (Sri Lanka)	ta-LK	Male	ta-LK-KumarNeural	General
Telugu (India)	te-IN	Female	te-IN-ShrutiNeural	General
Telugu (India)	te-IN	Male	te-IN-MohanNeural	General
Thai (Thailand)	th-TH	Female	th-TH-AcharaNeural	General
Thai (Thailand)	th-TH	Female	th-TH-PremwadeeNeural	General
Thai (Thailand)	th-TH	Male	th-TH-NiwatNeural	General
Turkish (Turkey)	tr-TR	Female	tr-TR-EmelNeural	General
Turkish (Turkey)	tr-TR	Male	tr-TR-AhmetNeural	General
Ukrainian (Ukraine)	uk-UA	Female	uk-UA-PolinaNeural	General
Ukrainian (Ukraine)	uk-UA	Male	uk-UA-OstapNeural	General
Urdu (India)	ur-IN	Female	ur-IN-GulNeural	General
Urdu (India)	ur-IN	Male	ur-IN-SalmanNeural	General
Urdu (Pakistan)	ur-PK	Female	ur-PK-UzmaNeural	General
Urdu (Pakistan)	ur-PK	Male	ur-PK-AsadNeural	General
Uzbek (Uzbekistan)	uz-UZ	Female	uz-UZ-MadinaNeural	General
Uzbek (Uzbekistan)	uz-UZ	Male	uz-UZ-SardorNeural	General
Vietnamese (Vietnam)	vi-VN	Female	vi-VN-HoaiMyNeural	General
Vietnamese (Vietnam)	vi-VN	Male	vi-VN-NamMinhNeural	General
Welsh (United Kingdom)	cy-GB	Female	cy-GB-NiaNeural	General
Welsh (United Kingdom)	cy-GB	Male	cy-GB-AledNeural	General
Zulu (South Africa)	zu-ZA	Female	zu-ZA-ThandoNeural	General
Zulu (South Africa)	zu-ZA	Male	zu-ZA-ThembaNeural	General";
            #endregion

            var m = Regex.Matches(text, @"(.*)\t(.*)\t(.*)\t(.*)\t(.*)");

            return m.Cast<Match>().Select(m => (m.Groups[1].Value, m.Groups[2].Value, m.Groups[3].Value, m.Groups[4].Value, m.Groups[5].Value)).ToArray();
        }

        private (string voice, string[] styles)[] GetVoiceStyles()
        {
            #region styles text from  https://docs.microsoft.com/en-us/azure/cognitive-services/speech-service/language-support?tabs=speechtotext
            var text = @"en-US-AriaNeural	angry, chat, cheerful, customerservice, empathetic, excited, friendly, hopeful, narration-professional, newscast-casual, newscast-formal, sad, shouting, terrified, unfriendly, whispering		
en-US-DavisNeural Public preview	angry, chat, cheerful, excited, friendly, hopeful, sad, shouting, terrified, unfriendly, whispering		
en-US-GuyNeural	angry, cheerful, excited, friendly, hopeful, newscast, sad, shouting, terrified, unfriendly, whispering		
en-US-JaneNeural Public preview	angry, cheerful, excited, friendly, hopeful, sad, shouting, terrified, unfriendly, whispering		
en-US-JasonNeural Public preview	angry, cheerful, excited, friendly, hopeful, sad, shouting, terrified, unfriendly, whispering		
en-US-JennyNeural	angry, assistant, chat, cheerful,customerservice, excited, friendly, hopeful, newscast, sad, shouting, terrified, unfriendly, whispering		
en-US-NancyNeural Public preview	angry, cheerful, excited, friendly, hopeful, sad, shouting, terrified, unfriendly, whispering		
en-US-SaraNeural	angry, cheerful, excited, friendly, hopeful, sad, shouting, terrified, unfriendly, whispering		
en-US-TonyNeural Public preview	angry, cheerful, excited, friendly, hopeful, sad, shouting, terrified, unfriendly, whispering		
fr-FR-DeniseNeural	cheerful, sad		
ja-JP-NanamiNeural	chat, cheerful, customerservice		
pt-BR-FranciscaNeural	calm		
zh-CN-XiaohanNeural	affectionate, angry, calm, cheerful, disgruntled, embarrassed, fearful, gentle, sad, serious	Supported	
zh-CN-XiaomoNeural	affectionate, angry, calm, cheerful, depressed, disgruntled, embarrassed, envious, fearful, gentle, sad, serious	Supported	Supported
zh-CN-XiaoruiNeural	angry, calm, fearful, sad	Supported	
zh-CN-XiaoshuangNeural	chat	Supported	
zh-CN-XiaoxiaoNeural	affectionate, angry, assistant, calm, chat, cheerful, customerservice, disgruntled, fearful, gentle, lyrical, newscast, poetry-reading, sad, serious	Supported	
zh-CN-XiaoxuanNeural	angry, calm, cheerful, depressed, disgruntled, fearful, gentle, serious	Supported	Supported
zh-CN-YunxiNeural	angry, assistant, cheerful, depressed, disgruntled, embarrassed, fearful, narration-relaxed, sad, serious	Supported	Supported
zh-CN-YunyangNeural	customerservice, narration-professional, newscast-casual	Supported	
zh-CN-YunyeNeural	angry, calm, cheerful, disgruntled, embarrassed, fearful, sad, serious	Supported	Supported
zh-CN-YunjianNeural Public preview	narration-relaxed, sports-commentary Public preview, sports-commentary-excited Public preview	Supported	
zh-CN-YunhaoNeural Public preview	general, advertisement-upbeat Public preview	Supported	
zh-CN-YunfengNeural Public preview	calm, angry, disgruntled, cheerful, fearful, sad, serious, depressed	Supported	";
            #endregion

            var matches = Regex.Matches(text.Replace(" Public preview", ""), "(.*)\t(.*)\t(.*)\t(.*)");
            return matches.Cast<Match>().Select(t => (t.Groups[1].Value, t.Groups[2].Value.Split(',', StringSplitOptions.RemoveEmptyEntries))).ToArray();
        }

        private void BuildSSMLAndSaveLatestConfig()
        {
            if (loaded)
            {
                var speak = new SSMLSpeak();
                speak.lang = "en-US";
                speak.version = "1.0";
                speak.voice = new speakVoice();
                speak.voice.name = cbVoice.SelectedValue.ToString();
                speak.voice.expressas = new expressas();
                speak.voice.expressas.style = cbSpeakingStyle.SelectedValue?.ToString();
                speak.voice.expressas.role = cbRolePlay.SelectedValue?.ToString();
                speak.voice.expressas.prosody = new prosody
                {
                    Value = txtInput.Text,
                    rate = MapValue(Convert.ToSingle(numSpeakingSpeed.Value), 0, 3, -100, 200) + "%",
                    pitch = MapValue(Convert.ToSingle(numPitch.Value), 0, 2, -50, 50) + "%"
                };
                var xmlser = new XmlSerializer(typeof(SSMLSpeak));
                using (var sw = new StringWriter())
                {
                    xmlser.Serialize(sw, speak);
                    txtSSML.Text = sw.ToString();
                }

                config.Pitch = numPitch.Value;
                config.Speed = numSpeakingSpeed.Value;
                config.Lang = cbLanguage.SelectedValue?.ToString();
                config.Voice = cbVoice.SelectedValue?.ToString();
                config.Style = cbSpeakingStyle.SelectedValue?.ToString();
                config.Role = cbRolePlay.SelectedValue?.ToString();
                config.MP3 = chkMP3.Checked;
                config.KeepWAV = chkKeepWav.Checked;
                config.Stero = chkStero.Checked;
                config.SaveConfig();

            }
        }

        private float MapValue(float x, float v1, float v2, float v3, float v4)
        {
            return ((x - v1) / (v2 - v1)) * (v4 - v3) + v3;
        }

        private async Task TTS()
        {
            // Creates an instance of a speech config with specified subscription key and service region.
            // Replace with your own subscription key and service region (e.g., "westus").
            var speechConfig = SpeechConfig.FromSubscription(config.Key, config.Region);

            // Set the voice name, refer to https://aka.ms/speech/voices/neural for full list.
            speechConfig.SpeechSynthesisVoiceName = cbVoice.Text;


            // Creates a speech synthesizer using the default speaker as audio output.
            using (var synthesizer = new SpeechSynthesizer(speechConfig))
            {
                using (var result = await synthesizer.SpeakSsmlAsync(txtSSML.Text))
                {
                    if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                    {
                        AudioDataStream stream = AudioDataStream.FromResult(result);  // to return in Memory
                        var tmpFile = Path.GetTempFileName();
                        var filename = GetFilename(txtInput.Text) + ".wav";
                        await stream.SaveToWaveFileAsync(tmpFile);

                        if (chkStero.Checked)
                        {
                            MonoToStero(tmpFile, filename);
                            File.Delete(tmpFile);
                        }
                        else
                        {
                            File.Move(tmpFile, filename);
                        }

                        //encode mp3
                        if (chkMP3.Checked)
                        {
                            await EncodeMP3(filename);

                            if (!chkKeepWav.Checked)
                            {
                                File.Delete(filename);
                            }
                        }
                    }
                    else if (result.Reason == ResultReason.Canceled)
                    {
                        var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);

                        if (cancellation.Reason == CancellationReason.Error)
                        {
                            //TODO: handle errors
                        }
                    }
                }
            }
        }

        private void MonoToStero(string source, string target)
        {
            using (var inputReader = new AudioFileReader(source))
            {
                // convert our mono ISampleProvider to stereo
                var stereo = new MonoToStereoSampleProvider(inputReader);
                stereo.LeftVolume = 1f; // full volume in left channel
                stereo.RightVolume = 1f; // full volume in right channel

                // ... OR ... could write the stereo audio out to a WAV file
                WaveFileWriter.CreateWaveFile16(target, stereo);
            }
        }

        private async Task EncodeMP3(string filename)
        {
            var file = await File.ReadAllBytesAsync(filename);
            var targetFile = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename) + ".mp3");

            using (var ms = new MemoryStream(file))
            using (var rdr = new WaveFileReader(ms))
            using (var wtr = new LameMP3FileWriter(targetFile, rdr.WaveFormat, 128))
            {
                await rdr.CopyToAsync(wtr);
                await wtr.FlushAsync();
            }
        }

        private string GetFilename(string text)
        {
            //remove invalid chars

            foreach (var item in Path.GetInvalidFileNameChars().Union(Path.GetInvalidFileNameChars()))
            {
                text = text.Replace(item, '_');
            }
            return Path.Combine(txtOutputFolder.Text, text.Substring(0, Math.Min(text.Length, 100)) + "_" + DateTime.Now.Ticks.ToString());
        }
    }
}
