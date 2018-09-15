USE [OnlineQuizDB]
GO
/****** Object:  View [dbo].[vDangKyThiChuanCNTTCoBan]    Script Date: 15/09/2018 4:33:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- 1. Sinh viên đăng ký thi Chuẩn CNTT cơ bản:

create view [dbo].[vDangKyThiChuanCNTTCoBan]
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
/****** Object:  View [dbo].[vDangKyThiChuanCNTTNangCao]    Script Date: 15/09/2018 4:33:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[vDangKyThiChuanCNTTNangCao]
as
select  AdvancedModuleRegistration.IDExaminee, Examinee.LastName, Examinee.FirstName, Examinee.Gender,Examinee.DateOfBirth, AdvancedModuleRegistration.QuestionModuleID ,InformationTechnologySkill.Name as InformationTechnologySkillName, QuestionModule.Name as QuestionModuleName ,
Examinee.IdentityCard, ExamScheduleAdvanced.ExaminationDate, StartEndTime.TimePeriod,StartEndTime.Remark, ExaminationRoom.Name as ExaminationRoomName
from Examinee, Registration, InformationTechnologySkill, AdvancedModuleRegistration, ExamScheduleAdvanced, ExamineeExamScheduleAdvanced,  StartEndTime, ExaminationRoom, QuestionModule
where InformationTechnologySkill.ID= Registration.InformationTechnologySkillID
and	   Registration.ExamineeID= Examinee.ID 
and      Registration.ID= AdvancedModuleRegistration.RegistrationID
and      AdvancedModuleRegistration.ID= ExamScheduleAdvanced.AdvancedModuleRegistrationID
and      ExamScheduleAdvanced.ID= ExamineeExamScheduleAdvanced.ExamScheduleAdvancedID
and      ExamScheduleAdvanced.StartEndTimeID= StartEndTime.ID
and      ExamScheduleAdvanced.ExaminationRoomID= ExaminationRoom.ID
and      AdvancedModuleRegistration.QuestionModuleID= QuestionModule.ID
GO
/****** Object:  View [dbo].[vLoginAdvanced]    Script Date: 15/09/2018 4:33:34 PM ******/
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
/****** Object:  View [dbo].[vLoginBasic]    Script Date: 15/09/2018 4:33:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[vLoginBasic]
	as
	select Examinee.*, IDExamineeRegistration.IDExaminee, IDExamineeRegistration.[Password]  from  Examinee , Registration, IDExamineeRegistration
	where  Examinee.ID= Registration.ExamineeID
	and     Registration.ID= IDExamineeRegistration.RegistrationID
GO
/****** Object:  StoredProcedure [dbo].[spGetDangKyThiChuanCNTTCoBan]    Script Date: 15/09/2018 4:33:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--2. Tạo thủ tục hiển thị thông tin thí sinh dự thi module cơ bản (theo IDExaminnee)
CREATE procedure  [dbo].[spGetDangKyThiChuanCNTTCoBan](
			@IDExaminee varchar(50)			
)
as
Begin
		select IDExamineeRegistration.IDExaminee, Examinee.LastName, Examinee.FirstName, Examinee.Gender,Examinee.DateOfBirth, InformationTechnologySkill.Name AS InformationTechnologySkillName,
		Examinee.IdentityCard, ExamScheduleBasic.ExaminationDate, StartEndTime.TimePeriod,StartEndTime.Remark , ExaminationRoom.Name as ExaminationRoomName
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

--3. Tạo thủ tục hiển thị thông tin thí sinh dự thi module nâng cao (theo IDExaminnee, Title)
GO
/****** Object:  StoredProcedure [dbo].[spGetDangKyThiChuanCNTTNangCao]    Script Date: 15/09/2018 4:33:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--4. Tạo thủ tục hiển thị thông tin thí sinh dự thi module nâng cao (theo IDExaminnee)

create procedure  [dbo].[spGetDangKyThiChuanCNTTNangCao](
	@IDExaminee varchar(50)
)
as
Begin
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
		and   AdvancedModuleRegistration.IDExaminee= @IDExaminee		
End
-- Hiển thị thông tin
-- Execute spGetDangKyThiChuanCNTTNangCao  'NC200918000003'
-- Execute spGetDangKyThiChuanCNTTNangCao  'NC200918000004'
-- Execute spGetDangKyThiChuanCNTTNangCao  'NC200918000005'
 
 -- 5. Tạo thủ tục lưu dữ liệu trước khi thi
GO
/****** Object:  StoredProcedure [dbo].[spGetDangKyThiChuanCNTTNangCaoByTitle]    Script Date: 15/09/2018 4:33:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure  [dbo].[spGetDangKyThiChuanCNTTNangCaoByTitle](
	@IDExaminee varchar(50),
	@Title             nvarchar(50)
)
as
Begin
		select  AdvancedModuleRegistration.IDExaminee, Examinee.LastName, Examinee.FirstName, Examinee.Gender,Examinee.DateOfBirth, AdvancedModuleRegistration.QuestionModuleID ,InformationTechnologySkill.Name as InformationTechnologySkillName, QuestionModule.Name as QuestionModuleName ,
		Examinee.IdentityCard, ExamScheduleAdvanced.ExaminationDate, StartEndTime.TimePeriod,StartEndTime.Remark, StartEndTime.TimePeriod + ' ('+ StartEndTime.Remark  + ')' as Title,ExaminationRoom.Name as ExaminationRoomName
		from Examinee, Registration, InformationTechnologySkill, AdvancedModuleRegistration, ExamScheduleAdvanced, ExamineeExamScheduleAdvanced,  StartEndTime, ExaminationRoom, QuestionModule
		where Examinee.ID= Registration.ExamineeID
		and    Registration.InformationTechnologySkillID= InformationTechnologySkill.ID
		and    Registration.ID= AdvancedModuleRegistration.RegistrationID
		and AdvancedModuleRegistration.ID= ExamScheduleAdvanced.AdvancedModuleRegistrationID
		and  ExamScheduleAdvanced.ID= ExamineeExamScheduleAdvanced.ExamScheduleAdvancedID
		and ExamScheduleAdvanced.StartEndTimeID= StartEndTime.ID
		and  ExamScheduleAdvanced.ExaminationRoomID= ExaminationRoom.ID
		and AdvancedModuleRegistration.QuestionModuleID= QuestionModule.ID
		and   AdvancedModuleRegistration.IDExaminee= @IDExaminee
		and   StartEndTime.TimePeriod + ' ('+ StartEndTime.Remark  + ')' =@Title
End
-- Hiển thị thông tin
-- Execute spGetDangKyThiChuanCNTTNangCaoByTitle 'NC200918000003', N'07h30-09h00 (Xuất 1)'
-- Execute spGetDangKyThiChuanCNTTNangCaoByTitle 'NC200918000004', N'09h30-11h00 (Xuất 2)'
-- Execute spGetDangKyThiChuanCNTTNangCaoByTitle 'NC200918000005', N'13h30-15h00 (Xuất 3)'
GO
/****** Object:  StoredProcedure [dbo].[spGetExaminationQuestion]    Script Date: 15/09/2018 4:33:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spGetExaminationQuestion]
(
		@ExamResultID				varchar(50),	
		@IDExaminee					varchar(50),
		@ExaminationID				varchar(50),
		@ExamCode					int				
)
as
Begin
		 select @ExaminationID= ExamResult.ExaminationID, @ExamCode	=ExamResult.ExamCode
		 from  ExamResult where  ExamResult.ID =@ExamResultID
		
		 select ExaminationQuestion.QuestionID, ExaminationQuestion.QuestionContent,
		 ExaminationQuestion.AAnswer, ExaminationQuestion.BAnswer,
		 ExaminationQuestion.CAnswer, ExaminationQuestion.DAnswer,
		 ExaminationQuestion.Answer, ExamResultDetail.ExamResultID,
		 ExaminationQuestion.ResultAnswer, ExamResultDetail.Answer AS MyAnswer
		from ExamResult, ExamResultDetail, ExaminationQuestion
		where    ExamResult.ID= ExamResultDetail.ExamResultID
		and      ExamResultDetail.QuesionID= ExaminationQuestion.QuestionID
		and      ExamResult.IDExaminee=@IDExaminee
		and      ExaminationQuestion.ExaminationID=@ExaminationID
		and      ExaminationQuestion.ExamCode= @ExamCode		
		
End

/* EXECUTE [spGetExaminationQuestion] 'B006B56C-343D-4A51-A5B0-51BF718AB464',
									'CB200918000001',
									'40B4E559-095E-4CD4-8E55-F2C69AF857BE',
									1
*/
GO
/****** Object:  StoredProcedure [dbo].[spInsertExamResultAndDetail]    Script Date: 15/09/2018 4:33:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spInsertExamResultAndDetail]
(
    @IDExaminee VARCHAR(50), -- Nhập vào Id dự thi	
    @ExamCode INT            -- Bốc đề ngẫu nhiên
)
AS
BEGIN
    DECLARE @ExamineeID VARCHAR(50);
    DECLARE @ExaminationID VARCHAR(255);
    DECLARE @Duration INT;
    IF (LEFT(@IDExaminee, 2) = 'CB')
    BEGIN
        SELECT @ExamineeID = Examinee.ID,
               @ExaminationID = Examination.ID,
               @Duration = Examination.Duration
        FROM Examinee,
             Registration,
             IDExamineeRegistration,
             ExamPeriod,
             ExaminationOfExamPeriod,
             Examination
        WHERE Examinee.ID = Registration.ExamineeID
              AND Registration.ID = IDExamineeRegistration.RegistrationID
              AND Registration.ExamPeriodID = ExamPeriod.ID
              AND ExamPeriod.ID = ExaminationOfExamPeriod.ExamPeriodID
              AND ExaminationOfExamPeriod.ExaminationID = Examination.ID
              AND Examination.InformationTechnologySkillID =
              (
                  SELECT ID
                  FROM InformationTechnologySkill
                  WHERE Name = N'Chuẩn kỹ năng sử dụng CNTT cơ bản'
              )
              AND IDExamineeRegistration.IDExaminee = @IDExaminee;

    END;
    ELSE
    BEGIN
        SELECT @ExamineeID = Examinee.ID,
               @ExaminationID = Examination.ID,
               @Duration = Examination.Duration
        FROM Examinee,
             Registration,
             AdvancedModuleRegistration,
             ExamPeriod,
             ExaminationOfExamPeriod,
             Examination
        WHERE Examinee.ID = Registration.ExamineeID
              AND Registration.ID = AdvancedModuleRegistration.RegistrationID
              AND Registration.ExamPeriodID = ExamPeriod.ID
              AND ExamPeriod.ID = ExaminationOfExamPeriod.ExamPeriodID
              AND ExaminationOfExamPeriod.ExaminationID = Examination.ID
              AND Examination.InformationTechnologySkillID =
              (
                  SELECT ID
                  FROM InformationTechnologySkill
                  WHERE Name = N'Chuẩn kỹ năng sử dụng CNTT nâng cao'
              )
              AND AdvancedModuleRegistration.IDExaminee = @IDExaminee;
    END;
    IF (LEN(@ExaminationID) != 0)
    BEGIN
		DECLARE @Count INT  = (SELECT COUNT(ID) AS Count FROM ExamResult 
		WHERE IDExaminee = @IDExaminee AND 
		ExaminationID = @ExaminationID AND 
		ExamCode = @ExamCode)

		IF(@Count = 0)
		BEGIN
			INSERT INTO ExamResult
			(
				ID,
				ExamineeID,
				IDExaminee,
				ExaminationID,
				ExamCode,
				Duration
			)
			OUTPUT Inserted.ID, Inserted.IDExaminee, Inserted.ExaminationID, Inserted.ExamCode, Inserted.Duration
			VALUES
			(NEWID(), @ExamineeID, @IDExaminee, @ExaminationID, @ExamCode, @Duration);
			INSERT INTO ExamResultDetail
			(
				ExamResultID,
				QuesionID
			)
			SELECT
				(
					SELECT ID FROM ExamResult WHERE IDExaminee = @IDExaminee
				),
				QuestionID
			FROM ExaminationQuestion
			WHERE ExaminationQuestion.ExaminationID = @ExaminationID
				  AND ExaminationQuestion.ExamCode = @ExamCode;
		END
        ELSE 
		SELECT ID, IDExaminee, ExaminationID, ExamCode, Duration FROM ExamResult 
		WHERE IDExaminee = @IDExaminee AND 
		ExaminationID = @ExaminationID AND 
		ExamCode = @ExamCode
    END;
END;
GO
/****** Object:  StoredProcedure [dbo].[spLogin]    Script Date: 15/09/2018 4:33:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- 1. Tạo spLogin
create procedure [dbo].[spLogin](
	@IDExaminee			varchar(50),
	@Password				varchar(255)
)
as
Begin
		if(LEFT(@IDExaminee,2)='CB')
		Begin 
				select Examinee.*, IDExamineeRegistration.IDExaminee, IDExamineeRegistration.[Password]  from  Examinee , Registration, IDExamineeRegistration
				where  Examinee.ID= Registration.ExamineeID
				and     Registration.ID= IDExamineeRegistration.RegistrationID
				and     IDExamineeRegistration.IDExaminee =@IDExaminee	
				and     IDExamineeRegistration.[Password] =@Password					
		End
		else
		Begin
					select Examinee.*, AdvancedModuleRegistration.IDExaminee,AdvancedModuleRegistration.[Password]   from  Examinee , Registration, AdvancedModuleRegistration
					where  Examinee.ID= Registration.ExamineeID
					and     Registration.ID= AdvancedModuleRegistration.RegistrationID
					and	   AdvancedModuleRegistration.IDExaminee=@IDExaminee
					and     AdvancedModuleRegistration.[Password] = @Password	
		End
End
/*
 execute  spLogin   @IDExaminee= 'CB200918000001',
								@Password   ='3244185981728979115075721453575112'
 execute  spLogin   @IDExaminee= 'CB200918000002',
								@Password   ='3244185981728979115075721453575112'
   
   execute  spLogin   @IDExaminee= 'NC200918000003',
								@Password   ='3244185981728979115075721453575112'
						
 execute  spLogin   @IDExaminee= 'NC200918000004',
								@Password   ='3244185981728979115075721453575112'
execute  spLogin   @IDExaminee= 'NC200918000005',
								@Password   ='3244185981728979115075721453575112'
   */
GO
/****** Object:  StoredProcedure [dbo].[spUpdateExamResultDetail]    Script Date: 15/09/2018 4:33:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spUpdateExamResultDetail](
		   @IDExaminee				varchar(50),
		   @ExamResultID			varchar(50),		   
		  @QuestionID					varchar(50), 
		  @Answer					    varchar(1),
		  @ResultAnswer			nvarchar(255),
		  @Duration						int 
)
as
Begin
	update ExamResultDetail set Answer= @Answer,  ResultAnswer=@ResultAnswer
	where ExamResultID=@ExamResultID and QuesionID=@QuestionID
	update ExamResult set Duration= @Duration where ID= @ExamResultID
End
GO
