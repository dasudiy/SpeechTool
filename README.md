# Speech Tool

[![licensebuttons by-nc-sa](https://licensebuttons.net/l/by-sa/4.0/88x31.png)](https://creativecommons.org/licenses/by-nc-sa/4.0)
[![.NET](https://github.com/dasudiy/SpeechTool/actions/workflows/dotnet.yml/badge.svg)](https://github.com/dasudiy/SpeechTool/actions/workflows/dotnet.yml)

一个可以调用Azure Speech Service的简单工具。具有以下功能：
1. 语音合成：可通过界面选择语言、声音，并自由修改SSML。可以保存wav文件并可通过NAudio+LAME编码为MP3文件。同时也可生成SRT字幕文件
2. 语音识别：可以选择视频或音频文件，识别其中语音，并可选择生成SRT字幕


> 需要自己的国际版Azure账号，可以在[Azure Portal](https://portal.azure.com)注册。
