USE [JPBM_DB]
GO

DROP TABLE ItemRifa
GO
DROP TABLE StatusPagamento
GO
DROP TABLE Rifa
GO
DROP TABLE StatusRifa
GO
DROP TABLE Contato
GO
DROP TABLE Cliente
GO
DROP TABLE TipoContato
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

----======================================CLIENTE=====================================
IF NOT EXISTS (SELECT TOP 1 1 FROM sys.tables WHERE name = 'TipoContato' ) 
BEGIN
	CREATE TABLE [dbo].[TipoContato] ( 
		[TipoContatoId]   [TINYINT]
		PRIMARY KEY IDENTITY(1, 1) NOT NULL, 
		[Descricao]       [VARCHAR](50) NOT NULL
	) 
	ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.tables WHERE name = 'Cliente' ) 
BEGIN
	CREATE TABLE [dbo].[Cliente] ( 
		[ClienteId]      [INT]
		PRIMARY KEY IDENTITY(1, 1) NOT NULL, 
		[Nome]           [VARCHAR](50) NOT NULL, 
		[Sobrenome]      [VARCHAR](100) NOT NULL, 
		[Vendedor]       [BIT] NOT NULL, 
		[DataCadastro]   [DATETIME] NOT NULL,
		[DataAlteracao]  [DATETIME] NULL
	) 
	ON [PRIMARY]

	ALTER TABLE [dbo].[Cliente]
	ADD CONSTRAINT DF_Cliente_DataCadastro DEFAULT GETDATE() FOR [DataCadastro]; 
	
	ALTER TABLE [dbo].[Cliente]
	ADD CONSTRAINT DF_Cliente_Vendedor DEFAULT 0 FOR [Vendedor]; 
END
GO

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.tables WHERE name = 'Contato' ) 
BEGIN
	CREATE TABLE [dbo].[Contato] ( 
		[ContatoId]       [INT]
		PRIMARY KEY IDENTITY(1, 1) NOT NULL, 
		[ClienteId]       [INT] NOT NULL, 
		[TipoContatoId]   [TINYINT] NOT NULL, 
		[Valor]           [VARCHAR](100) NOT NULL, 
		[Ativo]           [BIT] NOT NULL, 
		[DataCadastro]    [DATETIME] NOT NULL,
		[DataInativacao]      [DATETIME] NULL
	) 
	ON [PRIMARY]
	
	ALTER TABLE [dbo].[Contato]
	ADD CONSTRAINT FK_Contato_Cliente FOREIGN KEY(ClienteId) REFERENCES Cliente(
		ClienteId) ON DELETE CASCADE; 
	
	ALTER TABLE [dbo].[Contato]
	ADD CONSTRAINT FK_Contato_TipoContato FOREIGN KEY(TipoContatoId) REFERENCES TipoContato(
		TipoContatoId) ON DELETE CASCADE; 
	
	ALTER TABLE [dbo].[Contato]
	ADD CONSTRAINT DF_Contato_DataCadastro DEFAULT GETDATE() FOR [DataCadastro]; 
	
	ALTER TABLE [dbo].[Contato]
	ADD CONSTRAINT DF_Contato_Ativo DEFAULT 1 FOR [Ativo]; 
	
	ALTER TABLE [dbo].[Contato]
	ADD CONSTRAINT UC_Contato UNIQUE(ClienteId, TipoContatoId, Valor, Ativo); 
END
GO

----======================================RIFA=========================================
IF NOT EXISTS (SELECT TOP 1 1 FROM sys.tables WHERE name = 'StatusRifa' ) 
BEGIN
	CREATE TABLE [dbo].[StatusRifa] ( 
		[StatusRifaId]   [TINYINT]
		PRIMARY KEY IDENTITY(1, 1) NOT NULL, 
		[Descricao]      [VARCHAR](50) NOT NULL
	) 
	ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.tables WHERE name = 'Rifa' ) 
BEGIN
	CREATE TABLE [dbo].[Rifa] ( 
		[RifaId]         [INT]
		PRIMARY KEY IDENTITY(1, 1) NOT NULL, 
		[Tamanho]        [SMALLINT] NOT NULL, 
		[Nome]           [VARCHAR](100) NOT NULL,
		--[TipoRifaId] [tinyint] NOT NULL,
		--[DoadorId] [int] NOT NULL,
		[Premio]         [VARCHAR](100) NOT NULL, 
		[Valor]          [SMALLMONEY] NOT NULL, 
		[StatusRifaId]   [TINYINT] NOT NULL, 
		[DataCadastro]   [DATETIME] NOT NULL, 
		[DataInicio]     [DATETIME] NOT NULL, 
		[DataSorteio]    [DATETIME] NOT NULL
	) 
	ON [PRIMARY]
	
	ALTER TABLE [dbo].[Rifa]
	ADD CONSTRAINT FK_Rifa_StatusRifa FOREIGN KEY(StatusRifaId) REFERENCES StatusRifa(
		StatusRifaId) ON DELETE CASCADE; 
	
	ALTER TABLE [dbo].[Rifa]
	ADD CONSTRAINT DF_Rifa_DataCadastro DEFAULT GETDATE() FOR [DataCadastro]; 
	
	ALTER TABLE [dbo].[Rifa]
	ADD CONSTRAINT DF_Rifa_DataInicio DEFAULT GETDATE() FOR [DataInicio]; 
	
	ALTER TABLE [dbo].[Rifa]
	ADD CONSTRAINT CHK_Rifa_TamanhoValor CHECK(Tamanho > 0
											   AND Valor > 0); 
END
GO

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.tables WHERE name = 'StatusPagamento' ) 
BEGIN
	CREATE TABLE [dbo].[StatusPagamento] ( 
		[StatusPagamentoId]   [TINYINT]
		PRIMARY KEY IDENTITY(1, 1) NOT NULL, 
		[Descricao]           [VARCHAR](50) NOT NULL
	) 
	ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT TOP 1 1 FROM sys.tables WHERE name = 'ItemRifa' ) 
BEGIN	
	CREATE TABLE [dbo].[ItemRifa] ( 
		[ItemRifaId]          [BIGINT]
		PRIMARY KEY IDENTITY(1, 1) NOT NULL, 
		[RifaId]              [INT] NOT NULL, 
		[VendedorId]          [INT] NOT NULL, 
		[ClienteId]           [INT] NOT NULL, 
		[NumeroEscolhido]     [SMALLINT] NOT NULL, 
		[Sorteado]            [BIT] NOT NULL, 
		[StatusPagamentoId]   [TINYINT] NOT NULL, 
		[Ativo]               [BIT] NOT NULL, 
		[DataCadastro]        [DATETIME] NOT NULL, 
		[DataPagamento]       [DATETIME] NULL, 
		[DataAlteracao]       [DATETIME] NULL, 
		[DataInativacao]      [DATETIME] NULL
	) 
	ON [PRIMARY]
	
	ALTER TABLE [dbo].[ItemRifa]
	ADD CONSTRAINT FK_ItemRifa_Rifa FOREIGN KEY(RifaId) REFERENCES Rifa(
		RifaId) ON DELETE CASCADE;  
	
	ALTER TABLE [dbo].[ItemRifa]
	ADD CONSTRAINT FK_ItemRifa_Vendedor FOREIGN KEY(VendedorId) REFERENCES Cliente(
		ClienteId) ON DELETE NO ACTION; 
	
	ALTER TABLE [dbo].[ItemRifa]
	ADD CONSTRAINT FK_ItemRifa_Cliente FOREIGN KEY(ClienteId) REFERENCES Cliente(
		ClienteId) ON DELETE CASCADE;  
	
	ALTER TABLE [dbo].[ItemRifa]
	ADD CONSTRAINT FK_ItemRifa_StatusPagamento FOREIGN KEY(StatusPagamentoId) REFERENCES StatusPagamento(
		StatusPagamentoId) ON DELETE CASCADE;  
	
	ALTER TABLE [dbo].[ItemRifa]
	ADD CONSTRAINT DF_ItemRifa_Sorteado DEFAULT 0 FOR [Sorteado]; 
	
	--ALTER TABLE [dbo].[ItemRifa]
	--ADD CONSTRAINT DF_ItemRifa_StatusPagamentoId
	--DEFAULT 1 FOR [StatusPagamentoId]; 
	
	ALTER TABLE [dbo].[ItemRifa]
	ADD CONSTRAINT DF_ItemRifa_Ativo DEFAULT 1 FOR [Ativo]; 
	
	ALTER TABLE [dbo].[ItemRifa]
	ADD CONSTRAINT DF_ItemRifa_DataCadastro DEFAULT GETDATE() FOR [DataCadastro]; 
END	
GO


IF EXISTS(SELECT TOP 1 1 FROM sys.triggers WHERE name = 'TRG_INS_ItemRifa')
BEGIN
	DROP TRIGGER TRG_INS_ItemRifa
END
GO

CREATE TRIGGER [dbo].TRG_INS_ItemRifa ON [dbo].[ItemRifa]
INSTEAD OF INSERT
AS
	BEGIN
		-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;

		-- Insert statements for trigger here
		IF EXISTS ( SELECT TOP 1 
						1
					FROM 
						inserted AS I
						INNER JOIN ItemRifa AS IR ON I.RifaId = IR.RifaId
													 AND I.NumeroEscolhido = IR.NumeroEscolhido
					WHERE IR.Ativo = 1 AND IR.StatusPagamentoId <> 4
				  ) 
		BEGIN
			RAISERROR('Erro dados duplicados: Número já escolhido e ativo para esta rifa.', 10, 11);
		END
				ELSE
		BEGIN
			INSERT INTO ItemRifa ( 
				[RifaId], 
				[VendedorId], 
				[ClienteId], 
				[NumeroEscolhido], 
				[StatusPagamentoId],
				[DataCadastro], 
				[DataPagamento]
			) 
			SELECT 
				[RifaId], 
				[VendedorId], 
				[ClienteId], 
				[NumeroEscolhido], 
				[StatusPagamentoId],
				[DataCadastro], 
				[DataPagamento]
			FROM 
				 inserted
		END
	END

GO

IF NOT EXISTS(SELECT TOP 1 1 FROM Cliente)
BEGIN
	INSERT INTO TipoContato(Descricao) VALUES('Celular com DDD')
	INSERT INTO TipoContato(Descricao) VALUES('E-mail')
	
	INSERT INTO Cliente(Nome,Sobrenome)VALUES('Lucas','Lins')	
	INSERT INTO Cliente(Nome,Sobrenome,Vendedor)VALUES('Fernando','Garcia',1)
	
	INSERT INTO Contato(ClienteId,TipoContatoId,Valor) VALUES (1,1,'(11)9999-9999')
	INSERT INTO Contato(ClienteId,TipoContatoId,Valor) VALUES (1,2,'asdasdsad@gmail.com')
	
	INSERT INTO StatusRifa(Descricao) VALUES ('Ativa')
	INSERT INTO StatusRifa(Descricao) VALUES ('Cancelada')
	INSERT INTO StatusRifa(Descricao) VALUES ('Finalizada')
	
	INSERT INTO Rifa(Tamanho,Nome,Premio,Valor,StatusRifaId,DataCadastro,DataInicio,DataSorteio) VALUES (100,'Rifa Inicial','Cesta básica',5,1,GETDATE(),GETDATE(),GETDATE()+10)
	
	INSERT INTO StatusPagamento(Descricao) VALUES ('Aguardando Pagamento')
	INSERT INTO StatusPagamento(Descricao) VALUES ('Aguardando Depósito')
	INSERT INTO StatusPagamento(Descricao) VALUES ('Pagamento Recebido')
	INSERT INTO StatusPagamento(Descricao) VALUES ('Pagamento Estornado')
	
	INSERT INTO ItemRifa ( RifaId, VendedorId, ClienteId, NumeroEscolhido, StatusPagamentoId ) VALUES (1, 2, 1, 8, 1 )
	INSERT INTO ItemRifa ( RifaId, VendedorId, ClienteId, NumeroEscolhido, StatusPagamentoId ) VALUES (1, 2, 2, 20, 3 )
END



SELECT * FROM TipoContato
SELECT * FROM Contato
SELECT * FROM Cliente
SELECT * FROM StatusRifa
SELECT * FROM Rifa
SELECT * FROM ItemRifa
SELECT * FROM StatusPagamento

