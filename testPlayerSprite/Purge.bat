rem 参考サイト http://d.hatena.ne.jp/nakamura001/20090203/1233652705
cd /d %~dp0

rem del /s *.log
del /s *.csproj
del /s *.sln

rem 参考サイト http://tooljp.com/bat_qa/4B0ADEC0831C7D5F49257E4F000E1F58.html
rem 現在のディレクトリ配下の指定フォルダ削除するコマンド
rem コマンドプロンプトとバッチファイル内での記述の仕方は違うので注意する

rem インポートした Assets を保管するローカルのキャッシュ
for /R %%d in (Library) do rmdir /S /Q "%%d"
rem Build した時に一時的にできるファイルを保管する場所
for /R %%d in (Temp) do rmdir /S /Q "%%d"

for /R %%d in (Obj) do rmdir /S /Q "%%d"

for /R %%d in (.vs) do rmdir /S /Q "%%d"