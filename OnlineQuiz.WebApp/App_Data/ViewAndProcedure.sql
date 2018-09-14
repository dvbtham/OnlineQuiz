USE [OnlineQuizDB]
GO
/****** Object:  View [dbo].[vDangKyThiChuanCNTTCoBan]    Script Date: 14/09/2018 6:48:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[vDangKyThiChuanCNTTCoBan]
as
select IDExamineeRegistration.IDExaminee, Examinee.LastName, Examinee.FirstName, Examinee.Gender,Examinee.DateOfBirth, InformationTechnologySkill.Name,
Examinee.IdentityCard, ExamScheduleBasic.ExaminationDate, StartEndTime.TimePeriod,StartEndTime.Remark , ExaminationRoom.Name as ExaminationRoomName
from InformationTechnologySkill,  Registration, Examinee, ExamineeExamScheduleBasic, ExamScheduleBasic, StartEndTime, ExaminationRoom, IDExamineeRegistration
where InformationTechnologySkill.ID= Registration.InformationTechnologySkillID 
and Registration.ExamineeID= Examinee.ID
and Examinee.ID=ExamineeExamScheduleBasic.ExamineeID
and   ExamineeExamScheduleBasic.ExamScheduleBasicID= ExamScheduleBasic.ID
and  ExamScheduleBasic.StartEndTimeID= StartEndTime.ID
and  ExamScheduleBasic.ExaminationRoomID= ExaminationRoom.ID
and Registration.ID= IDExamineeRegistration.RegistrationID

--  Hiển thị Danh sách thí sinh đăng ký thi Chuẩn CNTT cơ bản
 -- select * from vDangKyThiChuanCNTTCoBan
 
-- 2. Sinh viên đăng ký thi Chuẩn CNTT nâng cao (Bắt buộc thi tối thiểu 3 lần thi -(mỗi module nâng cao 1 lần)
GO
/****** Object:  View [dbo].[vDangKyThiChuanCNTTNangCao]    Script Date: 14/09/2018 6:48:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vDangKyThiChuanCNTTNangCao]
as
select  AdvancedModuleRegistration.IDExaminee, Examinee.LastName, Examinee.FirstName, Examinee.Gender,Examinee.DateOfBirth, AdvancedModuleRegistration.QuestionModuleID ,InformationTechnologySkill.Name as InformationTechnologySkillName, QuestionModule.Name as QuestionModuleName ,
Examinee.IdentityCard, ExamScheduleAdvanced.ExaminationDate, StartEndTime.TimePeriod,StartEndTime.Remark, ExaminationRoom.Name as ExaminationRoomName
from Examinee, Registration, InformationTechnologySkill, AdvancedModuleRegistration, ExamScheduleAdvanced, ExamineeExamScheduleAdvanced,  StartEndTime, ExaminationRoom, QuestionModule
where Examinee.ID= Registration.ExamineeID
and    Registration.InformationTechnologySkillID= InformationTechnologySkill.ID
and    Registration.ID= AdvancedModuleRegistration.RegistrationID
and AdvancedModuleRegistration.ID= ExamScheduleAdvanced.AdvancedModuleRegistrationID
and  ExamScheduleAdvanced.ID= ExamineeExamScheduleAdvanced.ExamScheduleAdvancedID
and ExamScheduleAdvanced.StartEndTimeID= StartEndTime.ID
and  ExamScheduleAdvanced.ExaminationRoomID= ExaminationRoom.ID
and AdvancedModuleRegistration.QuestionModuleID= QuestionModule.ID
GO
/****** Object:  View [dbo].[vLoginAdvanced]    Script Date: 14/09/2018 6:48:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[vLoginAdvanced]
	as
	select Examinee.*, AdvancedModuleRegistration.IDExaminee,AdvancedModuleRegistration.[Password]   from  Examinee , Registration, AdvancedModuleRegistration
	where  Examinee.ID= Registration.ExamineeID
	and     Registration.ID= AdvancedModuleRegistration.RegistrationID
GO
/****** Object:  View [dbo].[vLoginBasic]    Script Date: 14/09/2018 6:48:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[vLoginBasic]
	as
	SELECT Examinee.*, IDExamineeRegistration.IDExaminee, IDExamineeRegistration.[Password]  
	FROM  Examinee , Registration, IDExamineeRegistration
	where  Examinee.ID= Registration.ExamineeID
	and     Registration.ID= IDExamineeRegistration.RegistrationID
GO
/****** Object:  StoredProcedure [dbo].[spGetCauHoiCuaKyThi]    Script Date: 14/09/2018 6:48:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spGetCauHoiCuaKyThi]
@ExamineeID VARCHAR(50)
AS
BEGIN
	
    DECLARE @ExamPeriodID UNIQUEIDENTIFIER =
            (
                SELECT ExamPeriodID
                FROM dbo.Registration 
				JOIN dbo.IDExamineeRegistration ON IDExamineeRegistration.RegistrationID = Registration.ID AND [Status] = 1
                WHERE ExamineeID = (SELECT TOP 1 dbo.Registration.ExamineeID FROM dbo.Registration
	JOIN dbo.IDExamineeRegistration ON IDExamineeRegistration.RegistrationID = Registration.ID
	WHERE IDExamineeRegistration.IDExaminee = @ExamineeID)
            );

    DECLARE @ExaminationID UNIQUEIDENTIFIER =
            (
                SELECT ID FROM dbo.Examination WHERE ExamPeriodID = @ExamPeriodID
            );

    SELECT *
    FROM dbo.ExaminationQuestion
    WHERE ExaminationID = @ExaminationID;
END;

-- EXECUTE dbo.spGetCauHoiCuaKyThi  @ExamineeID = 'CB200918000001'

GO
/****** Object:  StoredProcedure [dbo].[spGetDangKyThiChuanCNTTCoBan]    Script Date: 14/09/2018 6:48:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--1. Tạo thủ tục hiển thị thông tin thí sinh dự thi module cơ bản (theo IDExaminnee)
CREATE procedure  [dbo].[spGetDangKyThiChuanCNTTCoBan](
			@IDExaminee varchar(50)
)
as
Begin
		select IDExamineeRegistration.IDExaminee, 
		Examinee.LastName, Examinee.FirstName, Examinee.Gender,
		Examinee.DateOfBirth, InformationTechnologySkill.Name AS InformationTechnologySkillName,
		ExamScheduleBasic.ExaminationDate,  IdentityCard,
		StartEndTime.Remark + ' (' + StartEndTime.TimePeriod + ')' AS  Remark, 
		ExaminationRoom.Name as ExaminationRoomName
		from InformationTechnologySkill,  Registration, Examinee, ExamineeExamScheduleBasic, ExamScheduleBasic, StartEndTime, ExaminationRoom, IDExamineeRegistration
		where InformationTechnologySkill.ID= Registration.InformationTechnologySkillID 
		and Registration.ExamineeID= Examinee.ID
		and Examinee.ID=ExamineeExamScheduleBasic.ExamineeID
		and   ExamineeExamScheduleBasic.ExamScheduleBasicID= ExamScheduleBasic.ID
		and  ExamScheduleBasic.StartEndTimeID= StartEndTime.ID
		and  ExamScheduleBasic.ExaminationRoomID= ExaminationRoom.ID
		and Registration.ID= IDExamineeRegistration.RegistrationID
		and IDExamineeRegistration.IDExaminee= @IDExaminee
End 
-- Hiển thị thông tin
 --execute spGetDangKyThiChuanCNTTCoBan  'CB200918000001'
-- execute spGetDangKyThiChuanCNTTCoBan  'CB200918000002'

--2. Tạo thủ tục hiển thị thông tin thí sinh dự thi module nâng cao (theo IDExaminnee, Title)
GO
/****** Object:  StoredProcedure [dbo].[spGetDangKyThiChuanCNTTNangCao]    Script Date: 14/09/2018 6:48:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--3. Tạo thủ tục hiển thị thông tin thí sinh dự thi module nâng cao (theo IDExaminnee)

CREATE procedure  [dbo].[spGetDangKyThiChuanCNTTNangCao](
	@IDExaminee varchar(50)
)
as
Begin
		select  AdvancedModuleRegistration.IDExaminee,
		Examinee.LastName, Examinee.FirstName, Examinee.Gender,
		Examinee.DateOfBirth, AdvancedModuleRegistration.QuestionModuleID ,
		InformationTechnologySkill.Name as InformationTechnologySkillName, 
		QuestionModule.Name as QuestionModuleName,
		Examinee.IdentityCard, ExamScheduleAdvanced.ExaminationDate, 
		StartEndTime.Remark + ' ('+ StartEndTime.TimePeriod  + ')' as Remark,
		ExaminationRoom.Name as ExaminationRoomName
		from Examinee, Registration, InformationTechnologySkill, 
		AdvancedModuleRegistration, ExamScheduleAdvanced, 
		ExamineeExamScheduleAdvanced,  StartEndTime, 
		ExaminationRoom, QuestionModule
		where Examinee.ID= Registration.ExamineeID
		and    Registration.InformationTechnologySkillID= InformationTechnologySkill.ID
		and    Registration.ID= AdvancedModuleRegistration.RegistrationID
		and AdvancedModuleRegistration.ID= ExamScheduleAdvanced.AdvancedModuleRegistrationID
		and  ExamScheduleAdvanced.ID= ExamineeExamScheduleAdvanced.ExamScheduleAdvancedID
		and ExamScheduleAdvanced.StartEndTimeID= StartEndTime.ID
		and  ExamScheduleAdvanced.ExaminationRoomID= ExaminationRoom.ID
		and AdvancedModuleRegistration.QuestionModuleID= QuestionModule.ID
		and   AdvancedModuleRegistration.IDExaminee= @IDExaminee		
End
-- Hiển thị thông tin
-- Execute spGetDangKyThiChuanCNTTNangCao  'NC200918000003'
-- Execute spGetDangKyThiChuanCNTTNangCao  'NC200918000004'
-- Execute spGetDangKyThiChuanCNTTNangCao  'NC200918000005'
-- execute spGetDangKyThiChuanCNTTCoBan  'CB200918000001'
GO
/****** Object:  StoredProcedure [dbo].[spGetDangKyThiChuanCNTTNangCaoByTitle]    Script Date: 14/09/2018 6:48:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure  [dbo].[spGetDangKyThiChuanCNTTNangCaoByTitle](
	@IDExaminee varchar(50),
	@Title             nvarchar(50)
)
as
Begin
		select  AdvancedModuleRegistration.IDExaminee, Examinee.LastName, Examinee.FirstName, Examinee.Gender,Examinee.DateOfBirth, AdvancedModuleRegistration.QuestionModuleID ,InformationTechnologySkill.Name as InformationTechnologySkillName, QuestionModule.Name as QuestionModuleName ,
		Examinee.IdentityCard, ExamScheduleAdvanced.ExaminationDate, 
		StartEndTime.Remark + ' ('+ StartEndTime.TimePeriod  + ')' as Remark,
		ExaminationRoom.Name as ExaminationRoomName
		from Examinee, Registration, InformationTechnologySkill, 
		AdvancedModuleRegistration, ExamScheduleAdvanced, 
		ExamineeExamScheduleAdvanced,  StartEndTime, 
		ExaminationRoom, QuestionModule
		where Examinee.ID= Registration.ExamineeID
		and    Registration.InformationTechnologySkillID= InformationTechnologySkill.ID
		and    Registration.ID= AdvancedModuleRegistration.RegistrationID
		and AdvancedModuleRegistration.ID= ExamScheduleAdvanced.AdvancedModuleRegistrationID
		and  ExamScheduleAdvanced.ID= ExamineeExamScheduleAdvanced.ExamScheduleAdvancedID
		and ExamScheduleAdvanced.StartEndTimeID= StartEndTime.ID
		and  ExamScheduleAdvanced.ExaminationRoomID= ExaminationRoom.ID
		and AdvancedModuleRegistration.QuestionModuleID= QuestionModule.ID
		and   AdvancedModuleRegistration.IDExaminee= @IDExaminee
		and   dbo.QuestionModule.Name = @Title
End
-- Hiển thị thông tin
-- Execute spGetDangKyThiChuanCNTTNangCaoByTitle 'NC200918000003', N'Sử dụng bảng tính nâng cao'
GO
/****** Object:  StoredProcedure [dbo].[spLogin]    Script Date: 14/09/2018 6:48:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spLogin]
@IDExaminee VARCHAR(50),
@Password VARCHAR(50)
AS
BEGIN
DECLARE @Level VARCHAR(2) = SUBSTRING(@IDExaminee, 1, 2);

IF(@Level = 'CB')
	BEGIN
		PRINT 'CB'
		SELECT * FROM dbo.vLoginBasic 
		WHERE IDExaminee = @IDExaminee AND [Password] = @Password
	END
ELSE
	BEGIN
		PRINT 'NC'
		SELECT * FROM dbo.vLoginAdvanced 
		WHERE IDExaminee = @IDExaminee AND [Password] = @Password
	END 
END
GO
