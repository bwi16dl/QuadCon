@Developer

When you add the service references, the controller must be running.

Add the references according the following list:

	SR Name				|					Local URL						|						Remote URL
------------------------|---------------------------------------------------|---------------------------------------------------------------------------
TestService				|	net.tcp://localhost:9090/TestService/			|		net.tcp://wi-gate.technikum-wien.at:60937/TestService/		
GenericInfoService		|	net.tcp://localhost:9091/GenericInfoService/	|		net.tcp://wi-gate.technikum-wien.at:61037/GenericInfoService/
KodiService				|	net.tcp://localhost:9092/KodiService/			|		net.tcp://wi-gate.technikum-wien.at:61137/KodiService/		
WeatherService			|	net.tcp://localhost:9093/WeatherService/		|		net.tcp://wi-gate.technikum-wien.at:61237/WeatherService/	
AdminService			|	net.tcp://localhost:9094/AdminService/			|		net.tcp://wi-gate.technikum-wien.at:61337/AdminService/		


Afterwards, reconfigure all service references, to generate Generic Lists instead of Arrays!