USE [OnlineQuizDB]
GO
/****** Object:  UserDefinedFunction [dbo].[fcgetIDExamineeAdvancedModuleRegistration]    Script Date: 18/09/2018 9:12:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
-- Test
		select IDExaminee = dbo.fcgetIDExamineeIDExamineeRegistration('04566afd-e74a-41f8-b0e2-47f762eb8b44')
		select IDExaminee from IDExamineeRegistration
*/

--2.  Tạo mã tự động cho trường IDExaminee trong bảng AdvancedModuleRegistration
-- drop function fcgetIDExamineeAdvancedModuleRegistration
create function [dbo].[fcgetIDExamineeAdvancedModuleRegistration](
	@IDRegistration			varchar(50)
)
returns varchar(14)
as
Begin
		declare @value varchar(14);
		declare @ngay		varchar(6)	
		declare @RegistrationDate datetime
		select @RegistrationDate=RegistrationDate from 	Registration where ID=@IDRegistration
		select @ngay = SUBSTRING(CONVERT(varchar(10), @RegistrationDate,103),1,2) +
								 SUBSTRING(CONVERT(varchar(10), @RegistrationDate,103),4,2) +
								 SUBSTRING(CONVERT(varchar(10), @RegistrationDate,103),9,2)
								 from Registration 
								where ID = @IDRegistration									
		select @value= right(isnull(MAX(right(IDExaminee,6)),0),6)+ 1				
								from AdvancedModuleRegistration
				set @value= 'NC' +@ngay + Replicate('0',6-LEN(@value))+@value;
		return @value;
End
GO
/****** Object:  UserDefinedFunction [dbo].[fcgetIDExamineeIDExamineeRegistration]    Script Date: 18/09/2018 9:12:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--1.  Tạo mã tự động cho trường IDExaminee trong bảng IDExamineeRegistration
-- drop function fcgetIDExamineeIDExamineeRegistration
create function [dbo].[fcgetIDExamineeIDExamineeRegistration](
	@IDRegistration			varchar(50)
)
returns varchar(14)
as
Begin
		declare @value varchar(14);
		declare @ngay		varchar(6)	
		declare @RegistrationDate datetime
		select @RegistrationDate=RegistrationDate from 	Registration where ID=@IDRegistration
		select @ngay = SUBSTRING(CONVERT(varchar(10), @RegistrationDate,103),1,2) +
								 SUBSTRING(CONVERT(varchar(10), @RegistrationDate,103),4,2) +
								 SUBSTRING(CONVERT(varchar(10), @RegistrationDate,103),9,2)
								 from Registration 
								where ID = @IDRegistration									
		select @value= right(isnull(MAX(right(IDExaminee,6)),0),6)+ 1				
								from IDExamineeRegistration
				set @value= 'CB' +@ngay + Replicate('0',6-LEN(@value))+@value;
		return @value;
End
GO
/****** Object:  View [dbo].[vDangKyThiChuanCNTTCoBan]    Script Date: 18/09/2018 9:12:52 PM ******/
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
/****** Object:  View [dbo].[vDangKyThiChuanCNTTNangCao]    Script Date: 18/09/2018 9:12:52 PM ******/
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
/****** Object:  View [dbo].[vLoginAdvanced]    Script Date: 18/09/2018 9:12:52 PM ******/
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
/****** Object:  View [dbo].[vLoginBasic]    Script Date: 18/09/2018 9:12:52 PM ******/
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
/****** Object:  StoredProcedure [dbo].[spGeExamineeByIdentityCard]    Script Date: 18/09/2018 9:12:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
-- Test
   	execute		spUpdateExamineeByIdentityCard		@LastName		 = N'Lê Công',
																				@FirstName	 = N'Dao',
																				@DateOfBirth	 = '1992/09/12',
																				@Gender			 = 1,
																				@IdentityCard	 = '205025610',
																				@Mobile			 = '0901234567',
																				@Email			 = 'dao@gmail.com'																		
	select * from Examinee
*/
-- 12. Tạo thủ tục hiển thị thông tin Thí sinh dựa vào Số chứng minh nhân dân
	create procedure [dbo].[spGeExamineeByIdentityCard](
			@IdentityCard		varchar(15)
	)
	as
	Begin
		select   Examinee.ID, 
				    Examinee.LastName,
					Examinee.FirstName,
					Examinee.FullName,
					Examinee.DateOfBirth,
					Examinee.IdentityCard,
					Examinee.Mobile,
					Examinee.Email,
					Examinee.Remark,
					Examinee.[Status]					      
		from Examinee 
		where IdentityCard =@IdentityCard	
	End
GO
/****** Object:  StoredProcedure [dbo].[spGenerateIDExaminee]    Script Date: 18/09/2018 9:12:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spGenerateIDExaminee]
(
	@RegistrationId uniqueidentifier
)
AS
BEGIN
	SELECT dbo.fcgetIDExamineeIDExamineeRegistration(@RegistrationId) AS IDExaminee
END
GO
/****** Object:  StoredProcedure [dbo].[spGenerateIDExamineeAdvn]    Script Date: 18/09/2018 9:12:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[spGenerateIDExamineeAdvn]
(
	@RegistrationId uniqueidentifier
)
AS
BEGIN
	SELECT dbo.fcgetIDExamineeAdvancedModuleRegistration(@RegistrationId) AS IDExaminee
END
GO
/****** Object:  StoredProcedure [dbo].[spGetAdvancedModuleRegistrationByIdentityCardExamPeriodId]    Script Date: 18/09/2018 9:12:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	-- Test
	execute spGetRegistrationByIdentityCardExamPeriodId
	@IdentityCard	='123456799', 
	@ExamPeriodId=N'93fade04-44d3-4361-9cc8-4843008f8a16'		
*/
-- 17. Hiển thị kết quả đăng ký chuẩn CNTT nâng cao
	--  drop procedure spGetAdvancedModuleRegistrationByIdentityCardExamPeriodId
	CREATE procedure [dbo].[spGetAdvancedModuleRegistrationByIdentityCardExamPeriodId](
			@IdentityCard						varchar(15),
			@ExamPeriodId			UNIQUEIDENTIFIER		
	)
	as
	Begin
	select		Examinee.IdentityCard, 
					AdvancedModuleRegistration.IDExaminee,
					InformationTechnologySkill.Name as InformationTechnologySkillName, 
					QuestionModule.Name as  QuestionModuleName,
					ExamPeriod.Name as ExamPeriodName,
					convert(varchar(10), ExamPeriod.StartDate,103) + ' - '+
					convert(varchar(10), ExamPeriod.EndDate,103)  as TestDate
		from		Examinee,
					Registration, 
					AdvancedModuleRegistration,
					InformationTechnologySkill, 
					QuestionModule,
					ExamPeriod
		where  Examinee.IdentityCard									=@IdentityCard															and
					ExamPeriod.ID										= @ExamPeriodId													and					
					Examinee.ID												= Registration.ExamineeID											and
					Registration.ID											= AdvancedModuleRegistration.RegistrationID			and
					Registration.InformationTechnologySkillID= InformationTechnologySkill.ID								and
					QuestionModule.ID										=AdvancedModuleRegistration.QuestionModuleID	and
					Registration.ExamPeriodID							= ExamPeriod.ID
	End		
GO
/****** Object:  StoredProcedure [dbo].[spGetDangKyThiChuanCNTTCoBan]    Script Date: 18/09/2018 9:12:52 PM ******/
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
/****** Object:  StoredProcedure [dbo].[spGetDangKyThiChuanCNTTNangCao]    Script Date: 18/09/2018 9:12:52 PM ******/
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
/****** Object:  StoredProcedure [dbo].[spGetDangKyThiChuanCNTTNangCaoByTitle]    Script Date: 18/09/2018 9:12:52 PM ******/
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
/****** Object:  StoredProcedure [dbo].[spGetExaminationQuestion]    Script Date: 18/09/2018 9:12:52 PM ******/
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
		and      ExamResultDetail.QuestionID= ExaminationQuestion.QuestionID
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
/****** Object:  StoredProcedure [dbo].[spGetQuestionModuleByInformationTechnologySkillName]    Script Date: 18/09/2018 9:12:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spGetQuestionModuleByInformationTechnologySkillName](
			@InformationTechnologySkillName nvarchar(100)
)
as
Begin
	declare @ID varchar(50)
	select @ID =ID from InformationTechnologySkill where Name= @InformationTechnologySkillName
	select * from QuestionModule where InformationTechnologySkillID= @ID
End
GO
/****** Object:  StoredProcedure [dbo].[spGetRegistrationByIdentityCardExamPeriodId]    Script Date: 18/09/2018 9:12:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spGetRegistrationByIdentityCardExamPeriodId](
		@IdentityCard						varchar(15),
		@ExamPeriodId		UNIQUEIDENTIFIER
	)
	as
	Begin
		select   Examinee.IdentityCard, 
					IDExamineeRegistration.IDExaminee,
					InformationTechnologySkill.Name as InformationTechnologySkillName, 
					ExamPeriod.Name as ExamPeriodName ,
					convert(varchar(10), ExamPeriod.StartDate,103) + ' - '+
					convert(varchar(10), ExamPeriod.EndDate,103)  as TestDate
		from		Examinee,
					Registration, 
					IDExamineeRegistration,
					InformationTechnologySkill, 
					ExamPeriod
		where  Examinee.IdentityCard=@IdentityCard																and
					ExamPeriod.ID= @ExamPeriodId															and
					Examinee.ID= Registration.ExamineeID																and
					Registration.ID= IDExamineeRegistration.RegistrationID									and
					Registration.InformationTechnologySkillID= InformationTechnologySkill.ID	and					
					Registration.ExamPeriodID= ExamPeriod.ID
	End
GO
/****** Object:  StoredProcedure [dbo].[spInsertAdvancedModuleRegistration]    Script Date: 18/09/2018 9:12:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spInsertAdvancedModuleRegistration](
		@IdentityCard										varchar(15),
		@ExamPeriodName							nvarchar(255),
		@InformationTechnologySkillName	nvarchar(100),
		@QuestionModuleName						nvarchar(255),
		@RegistrationDate								datetime			
)
as
Begin
		declare @ExamineeID									varchar(50)
		declare @ExamPeriodID								varchar(50)
		declare @InformationTechnologySkillID	varchar(50)
		declare @QuestionModuleID						varchar(50)
		declare @RegistrationID								varchar(50)
		select @ExamineeID= ID 
				    from		Examinee 
				    where	IdentityCard=@IdentityCard
		select @ExamPeriodID= ID 
					from		ExamPeriod 
					where  Name=	@ExamPeriodName
		select @InformationTechnologySkillID= ID 
					from		InformationTechnologySkill 
					where	Name= @InformationTechnologySkillName
		select @QuestionModuleID= ID 
					from		QuestionModule
					where	Name= @QuestionModuleName					
		if(@ExamineeID !='' and @ExamPeriodID != '' and @InformationTechnologySkillID !='')
			Begin
					insert into Registration  values(NEWID(), @ExamineeID,  @InformationTechnologySkillID, @ExamPeriodID, @RegistrationDate)
						IF (@@ROWCOUNT = 0)  						
									Rollback	transaction						
						else
							Begin				
								select @RegistrationID	=ID 
										from Registration 
										where  ExamineeID								 = @ExamineeID									and  
													InformationTechnologySkillID = @InformationTechnologySkillID		and 
													ExamPeriodID							 = @ExamPeriodID
								insert into AdvancedModuleRegistration(ID, RegistrationID, QuestionModuleID, IDExaminee) values(NEWID(), @RegistrationID	, @QuestionModuleID, dbo.fcgetIDExamineeAdvancedModuleRegistration(@RegistrationID))
							end
			End
End
GO
/****** Object:  StoredProcedure [dbo].[spInsertExamResultAndDetail]    Script Date: 18/09/2018 9:12:52 PM ******/
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
			OUTPUT Inserted.ID, Inserted.IDExaminee, 
			Inserted.ExaminationID, Inserted.ExamCode, 
			Inserted.Duration, Inserted.Status
			VALUES
			(NEWID(), @ExamineeID, @IDExaminee, @ExaminationID, @ExamCode, @Duration);
			INSERT INTO ExamResultDetail
			(
				ExamResultID,
				QuestionID
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
		SELECT ID, IDExaminee, ExaminationID, ExamCode, Duration, Status FROM ExamResult 
		WHERE IDExaminee = @IDExaminee AND 
		ExaminationID = @ExaminationID AND 
		ExamCode = @ExamCode
    END;
END;

/* EXEC dbo.spInsertExamResultAndDetail @IDExaminee = 'CB200918000001', -- varchar(50)
                                     @ExamCode = 1     -- int
*/
GO
/****** Object:  StoredProcedure [dbo].[spInsertRegistration]    Script Date: 18/09/2018 9:12:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spInsertRegistration](
		@IdentityCard										varchar(15),
		@ExamPeriodName							nvarchar(255),
		@InformationTechnologySkillName	nvarchar(100),
		@RegistrationDate								datetime			
)
as
Begin
		declare @ExamineeID									varchar(50)
		declare @ExamPeriodID								varchar(50)
		declare @InformationTechnologySkillID	varchar(50)
		declare @RegistrationID								varchar(50)
		select @ExamineeID= ID 
				    from		Examinee 
				    where	IdentityCard=@IdentityCard
		select @ExamPeriodID= ID 
					from		ExamPeriod 
					where  Name=	@ExamPeriodName
		select @InformationTechnologySkillID= ID 
					from		InformationTechnologySkill 
					where	Name= @InformationTechnologySkillName
		if(@ExamineeID !='' and @ExamPeriodID != '' and @InformationTechnologySkillID !='')
			Begin
					insert into Registration  values(NEWID(), @ExamineeID,  @InformationTechnologySkillID, @ExamPeriodID, @RegistrationDate)
						IF (@@ROWCOUNT = 0)  						
									Rollback	transaction						
						else
							Begin				
								select @RegistrationID=ID 
										from Registration 
										where  ExamineeID								 = @ExamineeID									and  
													InformationTechnologySkillID = @InformationTechnologySkillID		and 
													ExamPeriodID							 = @ExamPeriodID
								insert into IDExamineeRegistration(RegistrationID, IDExaminee) values(@RegistrationID, dbo.fcgetIDExamineeIDExamineeRegistration(@RegistrationID))
							end
			End
End
GO
/****** Object:  StoredProcedure [dbo].[spLogin]    Script Date: 18/09/2018 9:12:52 PM ******/
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
/****** Object:  StoredProcedure [dbo].[spUpdateExamineeByIdentityCard]    Script Date: 18/09/2018 9:12:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
-- Test
	execute spInsertExaminee		@LastName		 = N'Lê Thị',
													@FirstName	 = N'Vân',
													@DateOfBirth	 = '1992/09/12',
													@Gender			 = 1,
													@IdentityCard	 =  '205025999',
													@Mobile			 = '0901234512',
													@Email			 = 'van@gmail.com'
	select * from Examinee								
*/
-- 11. Tạo thủ tục spUpdateExamineeByIdentityCard để sửa thông tin thí sinh dựa theo chứng minh nhân dân
-- drop   procedure spUpdateExamineeByIdentityCard
create procedure [dbo].[spUpdateExamineeByIdentityCard](
		@LastName			nvarchar(50),
		@FirstName		nvarchar(50),
		@DateOfBirth		date,
		@Gender				bit,
		@IdentityCard		varchar(15),
		@Mobile				varchar(15),
		@Email				varchar(255)
)
as
Begin
		update Examinee set LastName		= @LastName,  
										 FirstName		= @FirstName, 
										 FullName		= @LastName +' '+@FirstName, 
										 DateOfBirth	= @DateOfBirth, 
										 Gender			= @Gender,
										 Mobile			= @Mobile, 
										 Email				= @Email
		where IdentityCard = @IdentityCard	
End
GO
/****** Object:  StoredProcedure [dbo].[spUpdateExamResultDetail]    Script Date: 18/09/2018 9:12:52 PM ******/
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
