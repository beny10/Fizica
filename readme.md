Project for physics that measure how an capacitor is charging and makes a graphic.

Cum se foloseste:

1.Schema:
Un condensator conectat cu pinul Ground la Ground-ul lui Arduino,si celalalt pin legat de pinul A0 analog al lui Arduino si o rezistenta in paralel la pinul 6.
Rezistenta se pune in functie de condesatorului.De exemplu pentru un condensator de 500 de microFarazi am pus o rezistenta de 10k si timpul de citire de 50 de milisecunde.

2.Ajustare

Intervalul de citire se poate ajusta din codul de Arduino.
Aceste 2 variabile retin intervalul citirii:
#define microDelay 1
int interval = 800;

"interval" retine intervalul de citire si
"microDelay" retine daca se asteapta durata de interval in microsecunde sau milisecunde.Cand ii setat 1 atunci sta in microsecunde.

3.Instructiuni de folosire

Dupa ce a fost incarcat codul pe Arduino facand modificarile necesare pentru ajustare a timpului,Se deschide din folderul executabil fisier GraphDrawer
si se apasa pe "Chargind"-pentru inceperea incaracarii condensatorului si "Discharging" pentru descarcare.Dupa ce au fost luate datele se va deschide Exccel cu datele de la Arduino.

17.10 improvements commit
Charge/Discharge control,stop and start working,time is send,send data on 2 bytes,automatically delete older data