CREATE TABLE [dbo].[grams3pos] (
    [gram3id] INT NOT NULL,
    [seferid] INT NOT NULL,
    [perekid] INT NOT NULL,
    [pasukid] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([gram3id], [seferid], [perekid], [pasukid])
);

