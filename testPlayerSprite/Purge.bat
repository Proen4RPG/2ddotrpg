rem �Q�l�T�C�g http://d.hatena.ne.jp/nakamura001/20090203/1233652705
cd /d %~dp0

rem del /s *.log
del /s *.csproj
del /s *.sln

rem �Q�l�T�C�g http://tooljp.com/bat_qa/4B0ADEC0831C7D5F49257E4F000E1F58.html
rem ���݂̃f�B���N�g���z���̎w��t�H���_�폜����R�}���h
rem �R�}���h�v�����v�g�ƃo�b�`�t�@�C�����ł̋L�q�̎d���͈Ⴄ�̂Œ��ӂ���

rem �C���|�[�g���� Assets ��ۊǂ��郍�[�J���̃L���b�V��
for /R %%d in (Library) do rmdir /S /Q "%%d"
rem Build �������Ɉꎞ�I�ɂł���t�@�C����ۊǂ���ꏊ
for /R %%d in (Temp) do rmdir /S /Q "%%d"

for /R %%d in (Obj) do rmdir /S /Q "%%d"

for /R %%d in (.vs) do rmdir /S /Q "%%d"