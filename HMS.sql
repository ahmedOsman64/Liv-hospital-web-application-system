Create database HMS;
use HMS
Create table Users(
Userid int primary key,
FullName varchar(40),
Phone int,
Password varchar(15),
Status varchar(10),
Dor date

);

DROP TABLE Appointments;

select * from Users

Select Userid, FullName, Phone, Password, Status, cast(Dor as varchar(10)) as DateRegister from Users ;


CREATE TABLE Patients (
    PatientID INT PRIMARY KEY,
    FullName VARCHAR(40),
    DateOfBirth DATE,
    Gender VARCHAR(6) CHECK (Gender IN ('Male', 'Female')),
    Address VARCHAR(255),
    Phone VARCHAR(15) UNIQUE ,
    BloodType VARCHAR(5) CHECK (BloodType IN ('A+', 'A-', 'B+', 'B-', 'AB+', 'AB-', 'O+', 'O-')),
    CreatedAt DATE NOT NULL DEFAULT GETDATE()
);


select * from Patients



CREATE TABLE Doctors (
    DoctorID INT IDENTITY(1,1) PRIMARY KEY,            -- Auto-incremented primary key
    DoctorName NVARCHAR(100) NOT NULL,                  -- Doctor's full name
    Specialization NVARCHAR(100) NOT NULL,              -- Doctor's specialization
    ExperienceYears INT NOT NULL,                       -- Years of experience
    Gender NVARCHAR(10) NOT NULL,                       -- Gender of the doctor
    Phone NVARCHAR(20) NOT NULL,                        -- Phone number
    Email NVARCHAR(100) NOT NULL,                       -- Email address
    Nationality NVARCHAR(50),                           -- Nationality of the doctor
    WorkingHours NVARCHAR(50),                          -- Working hours
    ProfilePhotoPath NVARCHAR(200),                     -- Path to profile photo (if any)
    CreatedAt DATETIME DEFAULT GETDATE()                -- Timestamp when the record was created
);

select * from Doctors;

CREATE TABLE Contacts (
    ContactID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100),
    Email NVARCHAR(100),
    Phone NVARCHAR(20),
    Message NVARCHAR(MAX),
    ContactDate DATETIME DEFAULT GETDATE()
);
select * from Contacts;

CREATE TABLE Nurses (
    NurseID INT IDENTITY(1,1) PRIMARY KEY,
    NurseName NVARCHAR(100) NOT NULL,
    Department NVARCHAR(100) NOT NULL,
    Gender NVARCHAR(10) NOT NULL,
    Phone NVARCHAR(20) NOT NULL,
    Email NVARCHAR(100),
    WorkingHours NVARCHAR(50)
);

CREATE TABLE Rooms (
    RoomID INT PRIMARY KEY IDENTITY(1,1),
    RoomNumber VARCHAR(50) NOT NULL,
    RoomType VARCHAR(50) NOT NULL
);

CREATE TABLE Beds (
    BedID INT PRIMARY KEY IDENTITY(1,1),
    BedNumber VARCHAR(50) NOT NULL,
    RoomID INT NOT NULL,
    BedStatus VARCHAR(50) DEFAULT 'Available',
    FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID)
);
select * from Appointments


CREATE TABLE Appointments (
    AppointmentID INT PRIMARY KEY IDENTITY(1,1),
    PatientID INT NOT NULL,
    DoctorID INT NOT NULL,
    AppointmentDate DATETIME NOT NULL,
    Reason NVARCHAR(250) NULL,  -- Optional field for the reason
    CreatedAt DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Patient FOREIGN KEY (PatientID) REFERENCES Patients(PatientID),
    CONSTRAINT FK_Doctor FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID)
);

select * from Appointments;

CREATE TABLE BloodBank (
    BloodID INT PRIMARY KEY IDENTITY(1,1),
    BloodType VARCHAR(5) NOT NULL, -- e.g., 'A+', 'O-', etc.
    Quantity INT NOT NULL CHECK (Quantity >= 0),
    LastUpdated DATE NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Medicines (
    MedicineID INT PRIMARY KEY IDENTITY(1,1),
    MedicineName VARCHAR(100) NOT NULL,
    MedicineType VARCHAR(50) NOT NULL, -- e.g., Tablet, Syrup, Injection
    QuantityAvailable INT NOT NULL CHECK (QuantityAvailable >= 0),
    Manufacturer VARCHAR(100),
    ExpiryDate DATE NOT NULL,
    AddedDate DATE NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Billing (
    BillID INT PRIMARY KEY IDENTITY(1,1),
    PatientID INT NOT NULL,
    AppointmentID INT,
    TotalAmount DECIMAL(10, 2) NOT NULL,
    AmountPaid DECIMAL(10, 2) NOT NULL,
    PaymentMethod VARCHAR(50), -- e.g. Cash, Card, Insurance
    BillingDate DATE NOT NULL,
    Notes NVARCHAR(255),

    -- Foreign Keys
    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID),
    FOREIGN KEY (AppointmentID) REFERENCES Appointments(AppointmentID)
);

CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    BillID INT NOT NULL,
    PaymentDate DATE NOT NULL,
    AmountPaid DECIMAL(10, 2) NOT NULL,
    PaymentMethod VARCHAR(50) NOT NULL, -- e.g. Cash, Card, Insurance
    TransactionReference VARCHAR(100), -- Optional: e.g. bank or system reference
    Notes NVARCHAR(255), -- Optional notes

    -- Foreign Key to link payment to a bill
    FOREIGN KEY (BillID) REFERENCES Billing(BillID)
);


CREATE TABLE Pharmacy (
    MedicineID INT IDENTITY(1,1) PRIMARY KEY,
    MedicineName NVARCHAR(100),
    Category NVARCHAR(50),
    Quantity INT,
    Price DECIMAL(10, 2)
);
