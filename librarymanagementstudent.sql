-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jun 15, 2023 at 02:31 AM
-- Server version: 10.4.27-MariaDB
-- PHP Version: 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `librarymanagementstudent`
--

-- --------------------------------------------------------

--
-- Table structure for table `author`
--

CREATE TABLE `author` (
  `id` int(11) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Gender` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `author`
--

INSERT INTO `author` (`id`, `Name`, `Gender`) VALUES
(43, 'Squidward', 'Male'),
(44, 'MJ', 'Male'),
(45, 'Welt', 'Male');

-- --------------------------------------------------------

--
-- Table structure for table `category`
--

CREATE TABLE `category` (
  `CategoryID` int(100) NOT NULL,
  `Category` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `category`
--

INSERT INTO `category` (`CategoryID`, `Category`) VALUES
(1, 'Academic researches'),
(2, 'Archives'),
(3, 'Articles'),
(4, 'Audio recording'),
(5, 'Books'),
(6, 'Cases and jurisprudence'),
(7, 'Electronic resources'),
(8, 'Literature index'),
(9, 'Museum'),
(10, 'Serials'),
(11, 'Special materials'),
(12, 'Vertical files'),
(13, 'Video recording'),
(14, 'AI Generated');

-- --------------------------------------------------------

--
-- Table structure for table `department`
--

CREATE TABLE `department` (
  `DepartmentID` int(100) NOT NULL,
  `Department` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `department`
--

INSERT INTO `department` (`DepartmentID`, `Department`) VALUES
(1, 'School of Education'),
(2, 'School of Business Administration & Accountancy'),
(3, 'School of Hospitality & Tourism Management'),
(4, 'College of Arts & Science'),
(5, 'Allied & Medical Sciences'),
(6, 'College of Computer Science'),
(7, 'College of Criminal Justice Education');

-- --------------------------------------------------------

--
-- Table structure for table `location`
--

CREATE TABLE `location` (
  `LocationID` int(100) NOT NULL,
  `Location` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `location`
--

INSERT INTO `location` (`LocationID`, `Location`) VALUES
(1, 'Academic Researches'),
(2, 'Archives'),
(3, 'Articles'),
(4, 'Audio Recording'),
(5, 'Books'),
(6, 'Cases and Juriprudence'),
(7, 'Electronic Resources'),
(8, 'Literature Index'),
(9, 'Museum'),
(10, 'Seriels'),
(11, 'Serials'),
(12, 'Special Materials'),
(13, 'Vertical Files'),
(14, 'Video Recording'),
(15, 'Website'),
(17, 'Wumao');

-- --------------------------------------------------------

--
-- Table structure for table `publisher`
--

CREATE TABLE `publisher` (
  `id` int(11) NOT NULL,
  `Name` varchar(100) DEFAULT NULL,
  `Gender` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `publisher`
--

INSERT INTO `publisher` (`id`, `Name`, `Gender`) VALUES
(14, 'Boss Apo', 'Male'),
(17, 'Chris Brown', 'Male'),
(28, 'Werner', 'Male');

-- --------------------------------------------------------

--
-- Table structure for table `tblaccounts`
--

CREATE TABLE `tblaccounts` (
  `IDnumber` varchar(50) NOT NULL,
  `UserName` varchar(50) NOT NULL,
  `UserType` varchar(50) NOT NULL,
  `Password` varchar(50) NOT NULL,
  `DateCreated` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tblaccounts`
--

INSERT INTO `tblaccounts` (`IDnumber`, `UserName`, `UserType`, `Password`, `DateCreated`) VALUES
('21-00987', 'Gab', 'Librarian', '12345', '01-28-23');

-- --------------------------------------------------------

--
-- Table structure for table `tblbookassign`
--

CREATE TABLE `tblbookassign` (
  `Id` int(10) NOT NULL,
  `Library_ID` varchar(50) NOT NULL,
  `User` varchar(50) NOT NULL,
  `Accession` varchar(50) NOT NULL,
  `Title` varchar(50) NOT NULL,
  `Issue_Date` varchar(50) NOT NULL,
  `Expected_Return` varchar(50) NOT NULL,
  `Date_Return` varchar(50) DEFAULT NULL,
  `Status` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tblbookassign`
--

INSERT INTO `tblbookassign` (`Id`, `Library_ID`, `User`, `Accession`, `Title`, `Issue_Date`, `Expected_Return`, `Date_Return`, `Status`) VALUES
(1, '11-03042023153842', 'Jemille ', '20230403120830', 'A Conceptual introduction to chemistry', 'Tuesday, 18 April 2023', 'Tuesday, 25 April 2023', '2023-04-18 17:56:03', 'RETURNED'),
(2, '11-03042023153842', 'Jemille ', '20230403121333', '3D game animation for dummies', 'Tuesday, 18 April 2023', 'Tuesday, 25 April 2023', '2023-04-18 17:56:03', 'RETURNED'),
(3, '11-03032023174556', 'Renzelle Apolinario', '20230403120616', 'American world litereture', 'Wednesday, 19 April 2023', 'Wednesday, 26 April 2023', '2023-04-20 14:46:32', 'RETURNED'),
(4, '11-03032023174556', 'Renzelle Apolinario', '20230403120830', 'A Conceptual introduction to chemistry', 'Wednesday, 19 April 2023', 'Wednesday, 26 April 2023', '2023-04-20 14:46:32', 'RETURNED'),
(7, '11-03312023152146', 'Charlie Gutierrez', '20230403121613', 'A history of mathematics', 'Wednesday, 19 April 2023', 'Wednesday, 26 April 2023', '2023-04-20 14:46:32', 'RETURNED'),
(8, '11-03312023152146', 'Charlie Gutierrez', '20230403121746', 'Artificial intelligence in assistive technology fo', 'Wednesday, 19 April 2023', 'Wednesday, 26 April 2023', '2023-04-20 14:46:32', 'RETURNED'),
(9, '11-03032023174556', 'Renzelle Apolinario', '20230403121746', 'Artificial intelligence in assistive technology fo', 'Wednesday, 19 April 2023', 'Wednesday, 26 April 2023', '2023-04-20 14:46:32', 'RETURNED'),
(10, '11-03042023153842', 'Jemille ', '20230403120830', 'A Conceptual introduction to chemistry', 'Wednesday, 19 April 2023', 'Wednesday, 26 April 2023', '2023-04-20 14:46:32', 'RETURNED'),
(12, '11-03032023174556', 'Renzelle Apolinario', '20230403121613', 'A history of mathematics', 'Thursday, 20 April 2023', 'Thursday, 27 April 2023', '2023-04-20 14:46:32', 'RETURNED'),
(16, '11-03312023152146', 'Charlie Gutierrez', '20230403120616', 'American world litereture', 'Thursday, 20 April 2023', 'Thursday, 27 April 2023', '2023-06-03 16:12:47', 'RETURNED'),
(17, '11-03312023152146', 'Charlie Gutierrez', '20230403120830', 'A Conceptual introduction to chemistry', 'Thursday, 20 April 2023', 'Thursday, 27 April 2023', '2023-06-12 23:28:12', 'RETURNED'),
(21, 'Charlie Gutierrez', '11-03312023152146', '20230403120830', 'A Conceptual introduction to chemistry', 'Wednesday, 07 June 2023', 'Wednesday, 14 June 2023', '2023-06-12 23:28:12', 'RETURNED'),
(22, 'Charlie Gutierrez', '11-03312023152146', '20230403121333', '3D game animation for dummies', 'Wednesday, 07 June 2023', 'Wednesday, 14 June 2023', '2023-06-12 23:28:18', 'RETURNED'),
(24, 'Example', '11-03112023182034', '20230403120616', 'American world litereture', 'Wednesday, 14 June 2023', 'Wednesday, 28 June 2023', '2023-06-14 19:49:31', 'RETURNED');

-- --------------------------------------------------------

--
-- Table structure for table `tblbookrequest`
--

CREATE TABLE `tblbookrequest` (
  `Id` int(11) NOT NULL,
  `Accession` varchar(50) NOT NULL,
  `requestedBy` varchar(50) NOT NULL,
  `dateRequest` varchar(50) NOT NULL,
  `statusRequest` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tblbookrequest`
--

INSERT INTO `tblbookrequest` (`Id`, `Accession`, `requestedBy`, `dateRequest`, `statusRequest`) VALUES
(2, '20230403120830', 'Jemille ', '2023-04-19', 'REJECTED'),
(3, '20230403121333', 'Jemille ', '2023-04-19', 'APPROVED'),
(4, '20230403120616', 'Example', '2023-06-24', 'WAITING FOR APPROVAL');

-- --------------------------------------------------------

--
-- Table structure for table `tblbooksinformation`
--

CREATE TABLE `tblbooksinformation` (
  `Id` int(11) NOT NULL,
  `Accession` varchar(50) NOT NULL,
  `Title` varchar(50) NOT NULL,
  `Author` varchar(50) NOT NULL,
  `Publisher` varchar(50) NOT NULL,
  `Language` varchar(50) NOT NULL,
  `DatePublish` varchar(50) NOT NULL,
  `Subject` varchar(50) NOT NULL,
  `Type` varchar(50) NOT NULL,
  `Department` varchar(50) NOT NULL,
  `Location` varchar(50) NOT NULL,
  `Status` varchar(50) NOT NULL,
  `Quantity` varchar(50) NOT NULL,
  `Available` varchar(50) NOT NULL,
  `PageNumber` varchar(50) NOT NULL,
  `Image` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tblbooksinformation`
--

INSERT INTO `tblbooksinformation` (`Id`, `Accession`, `Title`, `Author`, `Publisher`, `Language`, `DatePublish`, `Subject`, `Type`, `Department`, `Location`, `Status`, `Quantity`, `Available`, `PageNumber`, `Image`) VALUES
(20, '20230403120616', 'American world litereture', 'Paul Giles', 'John wiley', 'English ', '2009-06-10', 'English', 'Books', 'School Of Education', 'Books', 'Available', '2', '2', '200', 'American world litereture.jpg'),
(21, '20230403120830', 'A Conceptual introduction to chemistry', 'Richard C. Bauer', 'Mc-Graw-Hill', 'English ', '2005-07-07', 'Science', 'Books', 'College Of Arts & Science', 'Books', 'Available', '3', '3', '150', 'A Conceptual introduction to chemistry.jpg'),
(22, '20230403121333', '3D game animation for dummies', 'Kelly Murdock', 'Wiley Publse', 'English ', '2005-11-23', 'Science', 'Books', 'College Of Computer Science', 'Books', 'Available', '2', '2', '140', '3D game animation for dummies.jpg'),
(23, '20230403121613', 'A history of mathematics', 'Katz J. Victor', 'Addison Wesley', 'English ', '2009-08-18', 'Math', 'Books', 'School Of Business Administration & Accountancy', 'Books', 'Available', '5', '4', '210', 'A history of mathematics.jpg'),
(24, '20230403121746', 'Artificial intelligence in assistive technology fo', 'Quijano S. Yolanda', 'Frank Smith', 'English ', '2019-05-21', 'Science', 'Books', 'College Of Computer Science', 'Articles', 'Available', '2', '2', '150', 'Artificial intelligence in assistive technology for students with disabilities.jpg'),
(25, '20230403122119', 'Neurotheranostics as personalized medicines', 'Kevadiya D. Bhavesh', 'Kellson Victor', 'English ', '2019-01-30', 'Science', 'Articles', 'Allied & Medical Sciences', 'Articles', 'Available', '2', '2', '50', 'Neurotheranostics as personalized medicines.jpg'),
(26, '20230403122252', 'A land apart', 'Christian Rahadiansyah', 'Asiana Disen', 'English ', '2015-10-21', 'Science', 'Articles', 'School Of Hospitality & Tourism Manageme', 'Articles', 'Available', '2', '2', '40', 'A land apart.jpg'),
(27, '20230414024007', 'Example', 'Katz J. Victor', 'Wiley Publse', 'English ', '1993-12-13', 'English', 'Cases and jurisprudence', 'College Of Arts & Science', 'Cases and jurisprudence', 'Available', '56', '', '68', 'The Mathematics of Infinity.jpg'),
(29, '20230424013241', 'College Algebra', 'Kevadiya D. Bhavesh', 'Boss Apo', 'English ', '2023-04-24', 'Math', 'Books', 'College Of Computer Science', 'Books', 'Available', '5', '', '250', 'check.gif');

-- --------------------------------------------------------

--
-- Table structure for table `tbluseraccount`
--

CREATE TABLE `tbluseraccount` (
  `Image` text NOT NULL,
  `ID_Number` varchar(50) NOT NULL,
  `Password` varchar(50) NOT NULL,
  `Library_Access_ID` varchar(50) NOT NULL,
  `Student_Name` varchar(50) NOT NULL,
  `Course` varchar(50) NOT NULL,
  `Email_ID` varchar(50) NOT NULL,
  `userType` varchar(50) NOT NULL,
  `Mobile_Number` varchar(50) NOT NULL,
  `Reg_Date` varchar(50) NOT NULL,
  `dateApproved` varchar(50) DEFAULT NULL,
  `approvedBy` varchar(50) NOT NULL,
  `Status` varchar(50) NOT NULL,
  `assignBy` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbluseraccount`
--

INSERT INTO `tbluseraccount` (`Image`, `ID_Number`, `Password`, `Library_Access_ID`, `Student_Name`, `Course`, `Email_ID`, `userType`, `Mobile_Number`, `Reg_Date`, `dateApproved`, `approvedBy`, `Status`, `assignBy`) VALUES
('pikapool.jpg', '19-00083', '12345', '11-03312023152146', 'Charlie Gutierrez', 'Computer Science', 'charlie.gutierrez@arellano.edu.ph', 'College Student', '0912345679', '2023-03-08', '31-03-2023', 'ADMIN - CIRCULATION', 'APPROVED', ''),
('1249600.jpg', '19-00317', '12345', '11-03042023153842', 'Jemille ', 'Computer Science', 'jemillegalarosa0@gmail.com', 'College Student', '0912345679', '2023-03-04', '04-03-2023', 'ADMIN - CIRCULATION', 'APPROVED', ''),
('library.png', '19-01179', '12345', '11-03032023174556', 'Renzelle Apolinario', 'Computer Science', 'renzelleellezner@gmail.com', 'College Student', '0912345679', '2023-03-03', '03-03-2023', 'ADMIN - CIRCULATION', 'APPROVED', ''),
('john.jfif', '192.168.254.254', '12345', '', 'John Wick', 'Test', 'Babayaga14@gmail.com', 'Faculty', 'Test', '2023-04-18', NULL, '', 'WAITING FOR APPROVAL', ''),
('1249600.jpg', '20-65981', '12345', '11-03072023180849', 'Jay R Torres', 'Computer Science', 'Example@gmail.com', 'Faculty', '1511561615', '2023-03-07', '07-03-2023', 'ADMIN - CIRCULATION', 'APPROVED', ''),
('attachment.png', 'Example', 'Example', '11-03112023182034', 'Example', 'Example', 'Example', 'Faculty', 'Example', '2023-03-09', '11-03-2023', 'ADMIN - CIRCULATION', 'APPROVED', ''),
('attachment (1).png', 'Example1', 'Example1', '11-00001', 'Example1', 'Example1', 'Example1', 'Faculty', 'Example1', '2023-03-09', '13-04-2023', 'ADMIN - CIRCULATION', 'APPROVED', ''),
('occupation.png', 'Example2', 'Example2', '11-00001', 'Example2', 'Example2', 'Example2', 'College Student', 'Example2', '2023-03-09', '13-04-2023', 'ADMIN - CIRCULATION', 'APPROVED', '');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `author`
--
ALTER TABLE `author`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `category`
--
ALTER TABLE `category`
  ADD PRIMARY KEY (`CategoryID`);

--
-- Indexes for table `department`
--
ALTER TABLE `department`
  ADD PRIMARY KEY (`DepartmentID`);

--
-- Indexes for table `location`
--
ALTER TABLE `location`
  ADD PRIMARY KEY (`LocationID`);

--
-- Indexes for table `publisher`
--
ALTER TABLE `publisher`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `tblaccounts`
--
ALTER TABLE `tblaccounts`
  ADD PRIMARY KEY (`IDnumber`);

--
-- Indexes for table `tblbookassign`
--
ALTER TABLE `tblbookassign`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `tblbookrequest`
--
ALTER TABLE `tblbookrequest`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `tblbooksinformation`
--
ALTER TABLE `tblbooksinformation`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `tbluseraccount`
--
ALTER TABLE `tbluseraccount`
  ADD PRIMARY KEY (`ID_Number`,`Library_Access_ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `author`
--
ALTER TABLE `author`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=46;

--
-- AUTO_INCREMENT for table `category`
--
ALTER TABLE `category`
  MODIFY `CategoryID` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT for table `department`
--
ALTER TABLE `department`
  MODIFY `DepartmentID` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `location`
--
ALTER TABLE `location`
  MODIFY `LocationID` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT for table `publisher`
--
ALTER TABLE `publisher`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT for table `tblbookassign`
--
ALTER TABLE `tblbookassign`
  MODIFY `Id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT for table `tblbookrequest`
--
ALTER TABLE `tblbookrequest`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `tblbooksinformation`
--
ALTER TABLE `tblbooksinformation`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
