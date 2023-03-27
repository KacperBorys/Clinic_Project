# Clinic_Project🩺

## Opis projektu🖊
Projekt przychodni wykonany w C# WPF to aplikacja desktopowa, która umożliwia zarządzanie danymi pacjentów i umówienia wizyt. W aplikacji zaimplementowano funkcjonalności takie jak rejestracja pacjentów, dodawanie i edytowanie ich danych, dodawanie wizyt oraz zarządzanie nimi. Aplikacja umożliwia również przeglądanie historii chorób pacjentów oraz ich wyników badań.
Aplikacja została zaprojektowana w sposób intuicyjny i użytkownik przy pomocy interfejsu graficznego może łatwo przeglądać, dodawać i edytować dane pacjentów oraz umawiać wizyty. Projekt przychodni wykonany w C# WPF umożliwia wygodne zarządzanie danymi pacjentów oraz zwiększa efektywność pracy personelu medycznego.

## Uruchomienie💻
W głównym okienku należy kliknąć kolejno Plik -> Otwórz -> przychodnia.xml
Dzięki czemu będziemy mieli możliwość zalogowania jako:
* ADMIN (login: ADMIN, hasło: ADMIN)
* PACJENT (login: 02463001875, hasło: password
* LEKARZ (login: 02453001875, hasło: 123123)

## Spis treści📓
* Aplikacja konsolowa (opis klas)
* Wizualizacja przychodni
* Aplikacja WPF (opis okienek)
* Funckje ADMINA
* Funkcje Pacjenta
* Funkcje Lekarza
* Zakończenie



## Aplikacja konsolowa🎮

### Klasa Osoba👤
* Klasa Osoba reprezentuje osobę, która może być lekarzem lub pacjentem w systemie medycznym. Klasa ta jest abstrakcyjna i zawiera pola takie jak imię, nazwisko, datę urodzenia, pesel, oraz płeć.

### Klasa Pacjent🙍
* Klasa Pacjent jest dziedziczącą po klasie Osoba klasą reprezentującą pacjenta w klinice. Klasa ta posiada listę historii wizyt pacjenta (typu List<Diagnoza>) oraz metody dodające i usuwające diagnozy z tej listy.

### Klasa Lekarz👨‍🔬
* Klasa "Lekarz" reprezentuje lekarza w systemie klinicznym. Dziedziczy po klasie "Osoba".Klasa posiada kilka pól, m.in. "specjalizacja", "godzinyPracy", "zaplanowane_Wizyty".

### Klasa ADMIN👨‍💼
* Klasa Admin to klasa reprezentująca administratora systemu w aplikacji Clinic_Project. Zawiera ona dwie właściwości statyczne typu string: login i haslo, które przechowują dane do logowania się administratora.

### Klasa Diagnoza💊
* Klasa Diagnoza reprezentuje diagnozę pacjenta, która jest przypisana do wizyty u lekarza. Klasa ta zawiera pola takie jak wizyta, choroba i recepta. Metoda ToString() służy do tworzenia listy diagnoz, która zawiera informacje o dacie wizyty, zdiagnozowanej chorobie i przepisanej recepcie.

### Klasa Wizyta📇
* Klasa Wizyta to klasa reprezentująca wizytę pacjenta u lekarza w systemie klinicznym.Klasa zawiera cztery pola: data, lekarza, pacjenta oraz godzinę.  Metoda ToString() służy do tworzenia informacji o wizycie: imieniu, nazwisku oraz peselu pacjenta jak i lekarza oraz konkretną datę i godzinę spotkania.

### Klasa Placówka🏠
* Klasa Placowka reprezentuje placówkę medyczną, która zawiera listy lekarzy, pacjentów i wizyt, a także godzinę otwarcia i zamknięcia placówki oraz słownik kont umożliwiający logowanie do systemu.
Klasa ta umożliwia serializację obiektów klasy do formatu XML jak i odczytu pliku XML.

### Klasy wyjątków DayException oraz HourException❗️
* Klasy wyjątków  służą do sygnalizowania błędów i nieprawidłowych sytuacji w programie. Są to specjalne klasy, które dziedziczą po klasie Exception lub jednej z jej pochodnych.

### Klasa Program (Main)▶️
* W tej klasie zostały utworzone instancje obiektów Pacjent, Lekarz, Wizyta oraz Diagnoza.Następnie zostały utworzone również konta dla tych obiektów.

## Wizualizacja przychodni📺
* Została ona wykonana przy użyciu platformy WPF. Jej wizualizację przedstawiają poniższe ilustracje:
![image](https://user-images.githubusercontent.com/101069553/228077461-be656194-fdf1-482f-a9ed-b8120409ca43.png)
![image](https://user-images.githubusercontent.com/101069553/228077767-c3e36140-65df-4eb3-9aa7-a1d475339aac.png)
![image](https://user-images.githubusercontent.com/101069553/228078601-b24174ac-b6a7-4935-896f-4de2381324cb.png)
![image](https://user-images.githubusercontent.com/101069553/228077560-c34a138e-02e4-4b82-a09b-4cab6b4f0d36.png)
![image](https://user-images.githubusercontent.com/101069553/228078691-b76ddf77-ff6f-480a-8c02-d76332dd4da8.png)
![image](https://user-images.githubusercontent.com/101069553/228078436-c3be9537-d4b2-4a4a-ad8b-fbf562d1bd08.png)

## Aplikacja WPF (opis okienek)🖥
W projekcie zostały wukorzystane następujące okienka
* MainWindow (Proces logowania się)
* Przychodnia_ADMIN (Funkcjonalności ADMINA)
* Przychodnia_Doktor (Funkcjonalności doktora)
* Przychodnia_Pacjent (Funkcjonalności pacjenta)
* Tworzenie_Konta_Pacjenta (Rejestracja pacjenta)
* Zmiana_Hasla (Proces zmiany hasła)

## Funkcje ADMINA👨‍💼
* Zarządzanie kontami użytkowników (dodawanie, usuwanie kont)
* Zarządzanie listą pacjentów
* Zarządzanie listą lekarzy
* Zarządzanie listą wizyt

## Funkcje Pacjenta🙍
* Rejestracja nowego konta pacjenta
* Przeglądanie swojego profilu i edycja swoich danych osobowych
* Przeglądanie wizyt oraz umawianie nowych wizyt u lekarza
* Przeglądanie swojej historii chorób oraz wyników badań medycznych
* Możliwość anulowania wizyt

## Funkcje Lekarza👨‍🔬
* Rejestracja nowego konta jako pacjent
* Przeglądanie swojego profilu i edycja swoich danych osobowych 
* Dodawanie nowych wizyt i przeglądanie listy swoich wizyt
* Przeglądanie historii chorób pacjentów oraz ich wyników badań medycznych



