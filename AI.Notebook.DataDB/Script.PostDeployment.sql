if not exists(select 1 from dbo.ResultTypes)
begin
	Insert into dbo.ResultTypes	([Name])
	Values ('Plain Text'), ('JSON'), ('URL'), ('Binary/Image'), ('Binary/Video'), ('Binary/Audio')
end

if not exists(select 1 from dbo.AIResources)
begin
	Insert into dbo.AIResources([Name])
	Values ('Speech Service'), ('Translator'), ('Computer Vision'), ('Language'), ('Search Service'), ('Face API')
end