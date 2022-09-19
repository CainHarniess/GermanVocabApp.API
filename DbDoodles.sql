SELECT
	 VL.Id
	,VL.[Name]	VocabListName
	,VL.DeletedDate
	,VLI.Id		VocabListId
	,VLI.WordType
	,VLI.IsWeakMasculineNoun
	,VLI.ReflexiveCase
	,VLI.Separability
	,VLI.Transitivity
	,VLI.ThirdPersonPresent
	,VLI.ThirdPersonImperfect
	,VLI.AuxiliaryVerb
	,VLI.Perfect
	,VLI.Gender
	,VLI.German
	,VLI.Plural
	,VLI.Preposition
	,VLI.PrepositionCase
	,VLI.Comparative
	,VLI.Superlative
	,VLI.English
	,VLI.FixedPlurality
	,VLI.CreatedDate
	,VLI.UpdatedDate
	,VLI.DeletedDate
FROM	dbo.VocabList	VL
JOIN	dbo.VocabListItem	VLI
	ON	VLI.VocabListID = VL.ID
WHERE	VL.Id = '59200d5a-c841-4e76-a030-08da99c8c827'