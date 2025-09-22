use master
go
create database DISCOS_DB
go
USE DISCOS_DB
GO
/****** Object:  Table [dbo].[ESTILOS]    Script Date: 10/14/2021 12:55:21 AM ******/
CREATE TABLE [dbo].[ESTILOS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](100) NULL,
 CONSTRAINT [PK_ESTILOS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
)
GO
/****** Object:  Table [dbo].[TIPOSEDICION]    Script Date: 10/14/2021 12:55:32 AM ******/
CREATE TABLE [dbo].[TIPOSEDICION](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_TIPOSEDICION] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
)
GO
/****** Object:  Table [dbo].[DISCOS]    Script Date: 10/14/2021 12:55:42 AM ******/
CREATE TABLE [dbo].[DISCOS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](100) NULL,
	[FechaLanzamiento] [smalldatetime] NULL,
	[CantidadCanciones] [int] NULL,
	[UrlImagenTapa] [varchar](200) NULL,
	[IdEstilo] [int] NULL,
	[IdTipoEdicion] [int] NULL,
 CONSTRAINT [PK_DISCOS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
)
GO

insert into estilos values ('Pop Punk')
insert into estilos values ('Pop')
insert into estilos values ('Rock')
insert into estilos values ('Grunge')
insert into TIPOSEDICION values ('Vinilo')
insert into TIPOSEDICION values ('CD')
insert into TIPOSEDICION values ('Tape')
insert into DISCOS values ('Pablo Honey', '01-01-1992', 12, 'https://cdns-images.dzcdn.net/images/cover/f08424290260e58c6d76275253b316fd/264x264.jpg', 2, 1)
insert into DISCOS values ('Siempre es hoy', '01-01-2002', 17, 'https://www.cmtv.com.ar/tapas-cd/ceratisiempre.jpg', 3, 3)