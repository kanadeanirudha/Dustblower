USE [Ver1]
GO
/****** Object:  UserDefinedFunction [dbo].[fGetPolicyAnswer]    Script Date: 1/22/2023 8:50:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER function [dbo].[fGetPolicyAnswer](@nsPolicyCode NVARCHAR(50),@nsCentreCode NVARCHAR(15)) returns nvarchar(100) 
AS
BEGIN
Declare @PolicyAnswer nVarchar(100);
	;with Cte1 as
	(
		SELECT 
			   A.ID
			  ,A.PolicyCode
			  ,A.PolicyName
			  ,A.PolicyDescription
			  ,A.PolicyRelatedToModuleCode
			  ,A.PolicyApplicableStatus
			  ,A.IsPolicyActive
			  ,b.PolicyAnsType
			  ,b.PolicyDefaultAnswer
			  ,b.PolicyQuestionDescription
			  ,b.PolicyRange
			  ,b.ID as GeneralPolicyRulesID

		FROM 
			GeneralPolicyMaster A
		inner join GeneralPolicyRules B on ( a.PolicyCode=b.PolicyCode and a.IsPolicyActive=1)
	), cte2 as 
	( select 
		cte1.ID
		,cte1.PolicyCode
		,cte1.PolicyName
		,cte1.PolicyDescription
		,cte1.PolicyRelatedToModuleCode
		,cte1.PolicyApplicableStatus
		,cte1.IsPolicyActive
		,cte1.PolicyAnsType
		,cte1.PolicyDefaultAnswer
		,cte1.PolicyQuestionDescription
		,cte1.PolicyRange 
		,cte1.GeneralPolicyRulesID
		,B.CentreCode 
	 from cte1
	 inner join    OrganisationStudyCentreMaster B on ( b.IsDeleted=0  )
	 ) , cte3 as 
	 (  select 
			cte2.ID
			,cte2.PolicyCode
			,cte2.PolicyDefaultAnswer
			,cte2.CentreCode 
			,cte2.GeneralPolicyRulesID
			,c.CentreCode as GeneralPolicyDetails_CentreCode
			,case when  c.CentreCode is null  then 0
			else 1
			end As CentreFlag
			,case when c.PolicyAnswered is null then cte2.PolicyDefaultAnswer
			      else c.PolicyAnswered
				  end PolicyAnswered
		from 
			cte2
		left outer join GeneralPolicyDetails C on ( 
													cte2.CentreCode=c.CentreCode and
													cte2.GeneralPolicyRulesID=c.GeneralPolicyRulesID
													And (GETUTCDATE() > = c.ApplicableFromDate and (c.ApplicableUptoDate is null or c.ApplicableUptoDate>=GETUTCDATE() ))
													)
		)
			Select 
					@PolicyAnswer = PolicyAnswered
			from cte3
			where cte3.CentreCode = @nsCentreCode
			and Cte3.PolicyCode = @nsPolicyCode

	 RETURN(isnull(@PolicyAnswer,0));
	End









