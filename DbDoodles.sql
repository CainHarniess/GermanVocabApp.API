SELECT
	 VL.Id
	,VL.[Name]	VocabListName
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
FROM	dbo.VocabList	VL
JOIN	dbo.VocabListItem	VLI
	ON	VLI.VocabListID = VL.ID
WHERE	VL.Name = 'WMN ReReTest'