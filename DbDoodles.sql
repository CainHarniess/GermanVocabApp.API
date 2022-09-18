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
	,VLI.CreatedDate
	,VLI.UpdatedDate
	,VLI.DeletedDate
FROM	dbo.VocabList	VL
JOIN	dbo.VocabListItem	VLI
	ON	VLI.VocabListID = VL.ID
--WHERE	VL.Id = 'a5789d9b-c103-4416-7171-08da99c811a0'