cd ./rpc/
python genc2h.py ../proto/client_call_hub/ csharp "" ../gen_svr/ ../proto/common
python genh2c.py ../proto/hub_call_client/ csharp "" ../gen_svr/ ../proto/common

python genc2h.py ../proto/client_call_hub/ csharp ../gen_cli/ "" ../proto/common
python genh2c.py ../proto/hub_call_client/ csharp ../gen_cli/ "" ../proto/common

cd ../
pause