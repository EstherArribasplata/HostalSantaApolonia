CREATE DATABASE [DB_HostalSantaApolonia]
GO

USE [DB_HostalSantaApolonia]
GO

CREATE TABLE [dbo].[Alquiler](
	[codAlquiler] [int] IDENTITY(1,1) NOT NULL,
	[codReserva] [int] NULL,
	[codCliente] [int] NULL,
	[codHabitacion] [int] NULL,
	[EstadoPago] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[codAlquiler] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Cliente](
	[codCliente] [int] IDENTITY(1,1) NOT NULL,
	[NombreC] [varchar](20) NULL,
	[ApellidosC] [varchar](20) NULL,
	[DNIC] [varchar](20) NULL,
	[TelefonoC] [varchar](20) NULL,
	[DireccionC] [varchar](20) NULL,
	[CorreoC] [varchar](20) NULL,
 CONSTRAINT [PK__Cliente__39F43E92AE8CB00F] PRIMARY KEY CLUSTERED 
(
	[codCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Habitacion](
	[codHabitacion] [int] IDENTITY(1,1) NOT NULL,
	[NumeroH] [char](50) NULL,
	[CostoH] [decimal](18, 0) NULL,
	[TipoH] [varchar](50) NULL,
	[NumeroCamas] [int] NULL,
	[EstadoH] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[codHabitacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Pago](
	[CodPago] [int] IDENTITY(1,1) NOT NULL,
	[codAlquiler] [int] NULL,
	[precioTotal] [decimal](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[CodPago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Reserva](
	[codReserva] [int] IDENTITY(1,1) NOT NULL,
	[FechaEntrada] [date] NULL,
	[FechaSalida] [date] NULL,
	[codAlquiler] [int] NULL,
	[codHabitacion] [int] NULL,
	[codCliente] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[codReserva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Usuario](
	[codUsuario] [int] IDENTITY(1,1) NOT NULL,
	[NombreU] [varchar](50) NULL,
	[ContrasenaU] [varchar](50) NULL,
	[CargoU] [int] NOT NULL,
	[DNIU] [varchar](50) NULL,
	[CelularU] [varchar](20) NULL,
 CONSTRAINT [PK__Usuario__52198B99979B8FAA] PRIMARY KEY CLUSTERED 
(
	[codUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Alquiler]  WITH CHECK ADD  CONSTRAINT [FK__Alquiler__codCli__412EB0B6] FOREIGN KEY([codCliente])
REFERENCES [dbo].[Cliente] ([codCliente])
GO

ALTER TABLE [dbo].[Alquiler] CHECK CONSTRAINT [FK__Alquiler__codCli__412EB0B6]
GO

ALTER TABLE [dbo].[Alquiler]  WITH CHECK ADD FOREIGN KEY([codHabitacion])
REFERENCES [dbo].[Habitacion] ([codHabitacion])
GO

ALTER TABLE [dbo].[Pago]  WITH CHECK ADD FOREIGN KEY([codAlquiler])
REFERENCES [dbo].[Alquiler] ([codAlquiler])
GO

ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Cliente] FOREIGN KEY([codCliente])
REFERENCES [dbo].[Cliente] ([codCliente])
GO

ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_Cliente]
GO

ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Habitacion] FOREIGN KEY([codHabitacion])
REFERENCES [dbo].[Habitacion] ([codHabitacion])
GO

ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_Habitacion]
GO

INSERT INTO [dbo].[Usuario] ([NombreU], [ContrasenaU], [CargoU], [DNIU], [CelularU])
VALUES ('admin', 'admin', 1, NULL, NULL);

INSERT INTO [dbo].[Usuario] ([NombreU], [ContrasenaU], [CargoU], [DNIU], [CelularU])
VALUES ('recep', 'recep', 2, NULL, NULL);

INSERT INTO [dbo].[Usuario] ([NombreU], [ContrasenaU], [CargoU], [DNIU], [CelularU])
VALUES ('limpi', 'limpi', 3, NULL, NULL);
