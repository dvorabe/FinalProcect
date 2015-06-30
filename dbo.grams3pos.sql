CREATE TABLE [dbo].[grams3pos] (
    [gram3id] INT NOT NULL,
    [seferid] VARCHAR(50) NOT NULL,
    [perekid] VARCHAR(50) NOT NULL,
    [pasukid] VARCHAR(50) NOT NULL,
    PRIMARY KEY CLUSTERED ([gram3id], [seferid], [perekid], [pasukid])
);

