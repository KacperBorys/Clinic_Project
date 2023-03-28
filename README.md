# Clinic_Project🩺

# Table of Contents📓
* Project Description
* Running the Application
* Console Application (Class Description)
* Clinic Visualization
* WPF Application (Window Description)
* ADMIN Functions
* Patient Functions
* Doctor Functions
* Conclusion
<br>

## Project Description🖊
The clinic project is developed in C# WPF - a desktop application that enables management of patient data and appointment scheduling. The application implements functionalities such as patient registration, adding and editing their information, adding appointments, and managing them. The application also allows viewing the patient's medical history and their test results.
The application is designed in an intuitive way, and the user can easily browse, add, and edit patient data and schedule appointments using the graphical user interface. The clinic project developed in C# WPF enables convenient management of patient data and increases the efficiency of medical staff.
<br>

## Running the Application💻
To run the application, compile the project in Visual Studio and run the executable file.<br>
In the main window, click File -> Open -> clinic.xml
This will allow us to log in as:

ADMIN (login: ADMIN, password: ADMIN) <br>
PATIENT (login: 02463001875, password: password) <br>
DOCTOR (login: 02453001875, password: 123123) <br>

## Console Application🎮
### Person Class👤
The Person class represents a person who can be a doctor or patient in the medical system. This class is abstract and contains fields such as name, surname, date of birth, PESEL, and gender.

### Patient Class🙍
The Patient class is a class that represents a patient in a clinic, inherited from the Person class. This class has a list of the patient's visit history (of type List<Diagnosis>) and methods for adding and removing diagnoses from this list.

### Doctor Class👨‍🔬
The Doctor class represents a doctor in the clinical system, inherited from the Person class. The class has several fields, including "specialization", "working hours", "scheduled_appointments".

### ADMIN Class👨‍💼
The ADMIN class represents the system administrator in the Clinic_Project application. It contains two static string properties: login and password, which store the administrator's login data.

### Diagnosis Class💊
The Diagnosis class represents the patient's diagnosis, which is assigned to a doctor's visit. This class contains fields such as visit, disease, and prescription. The ToString() method is used to create a list of diagnoses that contains information about the date of the visit, the diagnosed disease, and the prescribed prescription.

### Appointment Class📇
The Appointment class represents a patient's visit to a doctor in the clinical system. The class contains four fields: date, doctor, patient, and time. The ToString() method is used to create information about the appointment: the patient's first name, last name, and PESEL, as well as the doctor's name and PESEL, and the specific date and time of the meeting.

### Clinic Class🏠
The Clinic class represents a medical facility that includes lists of doctors, patients, and visits, as well as the opening and closing hours of the facility and a dictionary of accounts allowing logging into the system.
This class enables serialization of objects to XML format and reading from an XML file.

### Exception classes DayException and HourException❗️
Exception classes are used to signal errors and incorrect situations in the program. These are special classes that inherit from the Exception class or one of its derivatives.

### Program Class (Main)▶️
In this class, instances of the Pacjent, Lekarz, Wizyta, and Diagnoza objects were created. Accounts were also created for these objects.

## Clinic visualization📺
It was made using the WPF platform. Its visualization is presented in the following illustrations:
<br>

![image](https://user-images.githubusercontent.com/101069553/228077461-be656194-fdf1-482f-a9ed-b8120409ca43.png)
![image](https://user-images.githubusercontent.com/101069553/228077767-c3e36140-65df-4eb3-9aa7-a1d475339aac.png)
![image](https://user-images.githubusercontent.com/101069553/228078601-b24174ac-b6a7-4935-896f-4de2381324cb.png)
![image](https://user-images.githubusercontent.com/101069553/228077560-c34a138e-02e4-4b82-a09b-4cab6b4f0d36.png)
![image](https://user-images.githubusercontent.com/101069553/228078691-b76ddf77-ff6f-480a-8c02-d76332dd4da8.png)
![image](https://user-images.githubusercontent.com/101069553/228078436-c3be9537-d4b2-4a4a-ad8b-fbf562d1bd08.png)

## WPF Application (description of windows)🖥
The following windows were used in the project:
* MainWindow (Login process)
* Przychodnia_ADMIN (ADMIN's functionalities)
* Przychodnia_Doktor (Doctor's functionalities)
* Przychodnia_Pacjent (Patient's functionalities)
* Tworzenie_Konta_Pacjenta (Patient registration)
* Zmiana_Hasla (Password change process)

## ADMIN Functions👨‍💼
* Management of user accounts (adding, deleting accounts)
* Management of the list of patients
* Management of the list of doctors
* Management of the list of visits

## Patient Functions🙍
* Registration of a new patient account
* Viewing their profile and editing their personal data
* Viewing visits and making new appointments with a doctor
* Viewing their medical history and test results
* Ability to cancel visits

## Doctor Functions👨‍🔬
* Registering a new account as a patient
* Viewing their profile and editing their personal data
* Adding new visits and viewing their list of visits
* Viewing patients' medical history and test results

## Conclusion🔚
The clinic project was created in C# language with the use of graphical user interface (GUI). The aim of the project was to design a system that allows for patient, doctor, and admin registration, as well as the ability to view data, schedule appointments, and more. To create the project, I used classes that helped me organize the code and made it easier to work with data. I also implemented an authentication and authorization mechanism that protects patient medical data from unauthorized access. Additionally, interfaces such as IEquatable, ICloneable, IComparable, and Exception were used.<br>

I am satisfied with the project results and believe that I have achieved all of the goals.✅



