
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/24/2020 15:39:24
-- Generated from EDMX file: C:\Xamarin\N3\brot\BrotAPI_Final\Models\DBContextModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BorrarDBBrot];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__usuario_c__id_ca__5FB337D6]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[usuario_categoria] DROP CONSTRAINT [FK__usuario_c__id_ca__5FB337D6];
GO
IF OBJECT_ID(N'[dbo].[FK__usuario_c__id_us__5EBF139D]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[usuario_categoria] DROP CONSTRAINT [FK__usuario_c__id_us__5EBF139D];
GO
IF OBJECT_ID(N'[dbo].[fk_comentarios_post]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[comentarios] DROP CONSTRAINT [fk_comentarios_post];
GO
IF OBJECT_ID(N'[dbo].[FK_comentarios_users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[comentarios] DROP CONSTRAINT [FK_comentarios_users];
GO
IF OBJECT_ID(N'[dbo].[FK_interaccion_post_publicaciones]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[interaccion_post] DROP CONSTRAINT [FK_interaccion_post_publicaciones];
GO
IF OBJECT_ID(N'[dbo].[FK_interaccion_post_users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[interaccion_post] DROP CONSTRAINT [FK_interaccion_post_users];
GO
IF OBJECT_ID(N'[dbo].[FK_interaccion_post_users1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[interaccion_post] DROP CONSTRAINT [FK_interaccion_post_users1];
GO
IF OBJECT_ID(N'[dbo].[fk_like_comentario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[like_comentario] DROP CONSTRAINT [fk_like_comentario];
GO
IF OBJECT_ID(N'[dbo].[fk_like_comentario_users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[like_comentario] DROP CONSTRAINT [fk_like_comentario_users];
GO
IF OBJECT_ID(N'[dbo].[FK_like_post]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[like_post] DROP CONSTRAINT [FK_like_post];
GO
IF OBJECT_ID(N'[dbo].[FK_Like_user]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[like_post] DROP CONSTRAINT [FK_Like_user];
GO
IF OBJECT_ID(N'[dbo].[FK_post_user_creator]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[publicaciones] DROP CONSTRAINT [FK_post_user_creator];
GO
IF OBJECT_ID(N'[dbo].[FK_publicacion_guardada_publicaciones]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[publicacion_guardada] DROP CONSTRAINT [FK_publicacion_guardada_publicaciones];
GO
IF OBJECT_ID(N'[dbo].[FK_publicacion_guardada_users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[publicacion_guardada] DROP CONSTRAINT [FK_publicacion_guardada_users];
GO
IF OBJECT_ID(N'[dbo].[fk_seguidores_user]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[seguidores] DROP CONSTRAINT [fk_seguidores_user];
GO
IF OBJECT_ID(N'[dbo].[FK_seguidores_users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[seguidores] DROP CONSTRAINT [FK_seguidores_users];
GO
IF OBJECT_ID(N'[dbo].[fk_users_categoriaMain]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[users] DROP CONSTRAINT [fk_users_categoriaMain];
GO
IF OBJECT_ID(N'[dbo].[FK_visita_busqueda_users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[visita_busqueda] DROP CONSTRAINT [FK_visita_busqueda_users];
GO
IF OBJECT_ID(N'[dbo].[FK_visita_busqueda_users1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[visita_busqueda] DROP CONSTRAINT [FK_visita_busqueda_users1];
GO
IF OBJECT_ID(N'[dbo].[FK_visita_pefil_post_publicaciones]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[visita_pefil_post] DROP CONSTRAINT [FK_visita_pefil_post_publicaciones];
GO
IF OBJECT_ID(N'[dbo].[FK_visita_pefil_post_users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[visita_pefil_post] DROP CONSTRAINT [FK_visita_pefil_post_users];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[categoria]', 'U') IS NOT NULL
    DROP TABLE [dbo].[categoria];
GO
IF OBJECT_ID(N'[dbo].[codigos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[codigos];
GO
IF OBJECT_ID(N'[dbo].[comentarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[comentarios];
GO
IF OBJECT_ID(N'[dbo].[interaccion_post]', 'U') IS NOT NULL
    DROP TABLE [dbo].[interaccion_post];
GO
IF OBJECT_ID(N'[dbo].[like_comentario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[like_comentario];
GO
IF OBJECT_ID(N'[dbo].[like_post]', 'U') IS NOT NULL
    DROP TABLE [dbo].[like_post];
GO
IF OBJECT_ID(N'[dbo].[publicacion_guardada]', 'U') IS NOT NULL
    DROP TABLE [dbo].[publicacion_guardada];
GO
IF OBJECT_ID(N'[dbo].[publicaciones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[publicaciones];
GO
IF OBJECT_ID(N'[dbo].[seguidores]', 'U') IS NOT NULL
    DROP TABLE [dbo].[seguidores];
GO
IF OBJECT_ID(N'[dbo].[users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[users];
GO
IF OBJECT_ID(N'[dbo].[usuario_categoria]', 'U') IS NOT NULL
    DROP TABLE [dbo].[usuario_categoria];
GO
IF OBJECT_ID(N'[dbo].[visita_busqueda]', 'U') IS NOT NULL
    DROP TABLE [dbo].[visita_busqueda];
GO
IF OBJECT_ID(N'[dbo].[visita_pefil_post]', 'U') IS NOT NULL
    DROP TABLE [dbo].[visita_pefil_post];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'categoria'
CREATE TABLE [dbo].[categoria] (
    [id_categoria] int IDENTITY(1,1) NOT NULL,
    [nombre] varchar(25)  NOT NULL,
    [img] varchar(25)  NOT NULL
);
GO

-- Creating table 'codigos'
CREATE TABLE [dbo].[codigos] (
    [codigo] varchar(20)  NOT NULL,
    [id_user] int  NULL
);
GO

-- Creating table 'comentarios'
CREATE TABLE [dbo].[comentarios] (
    [id_comentario] int IDENTITY(1,1) NOT NULL,
    [id_user] int  NOT NULL,
    [id_post] int  NOT NULL,
    [contenido] varchar(200)  NULL,
    [fecha_creacion] datetime  NULL,
    [isDeleted] bit  NULL
);
GO

-- Creating table 'interaccion_post'
CREATE TABLE [dbo].[interaccion_post] (
    [id_interaccion_post] int IDENTITY(1,1) NOT NULL,
    [id_post] int  NOT NULL,
    [id_userqueinteractuo] int  NOT NULL,
    [id_perfilvisitado] int  NOT NULL,
    [fecha] datetime  NULL
);
GO

-- Creating table 'like_comentario'
CREATE TABLE [dbo].[like_comentario] (
    [id_like_comentario] int IDENTITY(1,1) NOT NULL,
    [id_user] int  NOT NULL,
    [id_comentario] int  NOT NULL,
    [fecha] datetime  NULL
);
GO

-- Creating table 'like_post'
CREATE TABLE [dbo].[like_post] (
    [id] int IDENTITY(1,1) NOT NULL,
    [id_user] int  NOT NULL,
    [id_post] int  NOT NULL,
    [fecha] datetime  NULL
);
GO

-- Creating table 'publicacion_guardada'
CREATE TABLE [dbo].[publicacion_guardada] (
    [id_publicacion_guardada] int IDENTITY(1,1) NOT NULL,
    [id_user] int  NOT NULL,
    [id_post] int  NOT NULL,
    [fecha] datetime  NULL
);
GO

-- Creating table 'publicaciones'
CREATE TABLE [dbo].[publicaciones] (
    [id_post] int IDENTITY(1,1) NOT NULL,
    [id_user] int  NOT NULL,
    [img] varchar(100)  NULL,
    [isImg] bit  NULL,
    [descripcion] varchar(300)  NULL,
    [fecha_creacion] datetime  NULL,
    [fecha_actualizacion] datetime  NULL,
    [isDeleted] bit  NULL
);
GO

-- Creating table 'seguidores'
CREATE TABLE [dbo].[seguidores] (
    [id_seguidores] int IDENTITY(1,1) NOT NULL,
    [seguidor_id] int  NOT NULL,
    [id_seguido] int  NOT NULL,
    [accepted] bit  NULL,
    [fecha] datetime  NULL
);
GO

-- Creating table 'users'
CREATE TABLE [dbo].[users] (
    [id_user] int IDENTITY(1,1) NOT NULL,
    [username] varchar(50)  NOT NULL,
    [puesto_name] varchar(50)  NULL,
    [nombre] varchar(50)  NULL,
    [apellido] varchar(50)  NULL,
    [descripcion] varchar(200)  NULL,
    [pass] varchar(50)  NOT NULL,
    [isVendor] bit  NOT NULL,
    [puntaje] int  NULL,
    [email] varchar(50)  NULL,
    [xlat] real  NULL,
    [ylon] real  NULL,
    [isActive] bit  NULL,
    [dui] varchar(15)  NULL,
    [num_telefono] varchar(15)  NULL,
    [img] varchar(100)  NULL,
    [isDeleted] bit  NULL,
    [Device_id] varchar(50)  NULL,
    [Phone_OS] varchar(8)  NULL,
    [id_categoria] int  NULL
);
GO

-- Creating table 'usuario_categoria'
CREATE TABLE [dbo].[usuario_categoria] (
    [id_usuario_categoria] int IDENTITY(1,1) NOT NULL,
    [id_usuario] int  NOT NULL,
    [id_categoria] int  NOT NULL,
    [isPrimary] int  NOT NULL
);
GO

-- Creating table 'visita_busqueda'
CREATE TABLE [dbo].[visita_busqueda] (
    [id_visita_busqueda] int IDENTITY(1,1) NOT NULL,
    [id_userquebusco] int  NOT NULL,
    [id_perfilvisitado] int  NOT NULL,
    [fecha] datetime  NULL
);
GO

-- Creating table 'visita_pefil_post'
CREATE TABLE [dbo].[visita_pefil_post] (
    [id_visita_pefil_post] int IDENTITY(1,1) NOT NULL,
    [id_post] int  NOT NULL,
    [id_userquevisito] int  NOT NULL,
    [id_perfilvisitado] int  NOT NULL,
    [fecha] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id_categoria] in table 'categoria'
ALTER TABLE [dbo].[categoria]
ADD CONSTRAINT [PK_categoria]
    PRIMARY KEY CLUSTERED ([id_categoria] ASC);
GO

-- Creating primary key on [codigo] in table 'codigos'
ALTER TABLE [dbo].[codigos]
ADD CONSTRAINT [PK_codigos]
    PRIMARY KEY CLUSTERED ([codigo] ASC);
GO

-- Creating primary key on [id_comentario] in table 'comentarios'
ALTER TABLE [dbo].[comentarios]
ADD CONSTRAINT [PK_comentarios]
    PRIMARY KEY CLUSTERED ([id_comentario] ASC);
GO

-- Creating primary key on [id_interaccion_post] in table 'interaccion_post'
ALTER TABLE [dbo].[interaccion_post]
ADD CONSTRAINT [PK_interaccion_post]
    PRIMARY KEY CLUSTERED ([id_interaccion_post] ASC);
GO

-- Creating primary key on [id_like_comentario] in table 'like_comentario'
ALTER TABLE [dbo].[like_comentario]
ADD CONSTRAINT [PK_like_comentario]
    PRIMARY KEY CLUSTERED ([id_like_comentario] ASC);
GO

-- Creating primary key on [id] in table 'like_post'
ALTER TABLE [dbo].[like_post]
ADD CONSTRAINT [PK_like_post]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id_publicacion_guardada] in table 'publicacion_guardada'
ALTER TABLE [dbo].[publicacion_guardada]
ADD CONSTRAINT [PK_publicacion_guardada]
    PRIMARY KEY CLUSTERED ([id_publicacion_guardada] ASC);
GO

-- Creating primary key on [id_post] in table 'publicaciones'
ALTER TABLE [dbo].[publicaciones]
ADD CONSTRAINT [PK_publicaciones]
    PRIMARY KEY CLUSTERED ([id_post] ASC);
GO

-- Creating primary key on [id_seguidores] in table 'seguidores'
ALTER TABLE [dbo].[seguidores]
ADD CONSTRAINT [PK_seguidores]
    PRIMARY KEY CLUSTERED ([id_seguidores] ASC);
GO

-- Creating primary key on [id_user] in table 'users'
ALTER TABLE [dbo].[users]
ADD CONSTRAINT [PK_users]
    PRIMARY KEY CLUSTERED ([id_user] ASC);
GO

-- Creating primary key on [id_usuario_categoria] in table 'usuario_categoria'
ALTER TABLE [dbo].[usuario_categoria]
ADD CONSTRAINT [PK_usuario_categoria]
    PRIMARY KEY CLUSTERED ([id_usuario_categoria] ASC);
GO

-- Creating primary key on [id_visita_busqueda] in table 'visita_busqueda'
ALTER TABLE [dbo].[visita_busqueda]
ADD CONSTRAINT [PK_visita_busqueda]
    PRIMARY KEY CLUSTERED ([id_visita_busqueda] ASC);
GO

-- Creating primary key on [id_visita_pefil_post] in table 'visita_pefil_post'
ALTER TABLE [dbo].[visita_pefil_post]
ADD CONSTRAINT [PK_visita_pefil_post]
    PRIMARY KEY CLUSTERED ([id_visita_pefil_post] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [id_categoria] in table 'usuario_categoria'
ALTER TABLE [dbo].[usuario_categoria]
ADD CONSTRAINT [FK__usuario_c__id_ca__5FB337D6]
    FOREIGN KEY ([id_categoria])
    REFERENCES [dbo].[categoria]
        ([id_categoria])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__usuario_c__id_ca__5FB337D6'
CREATE INDEX [IX_FK__usuario_c__id_ca__5FB337D6]
ON [dbo].[usuario_categoria]
    ([id_categoria]);
GO

-- Creating foreign key on [id_categoria] in table 'users'
ALTER TABLE [dbo].[users]
ADD CONSTRAINT [fk_users_categoriaMain]
    FOREIGN KEY ([id_categoria])
    REFERENCES [dbo].[categoria]
        ([id_categoria])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_users_categoriaMain'
CREATE INDEX [IX_fk_users_categoriaMain]
ON [dbo].[users]
    ([id_categoria]);
GO

-- Creating foreign key on [id_post] in table 'comentarios'
ALTER TABLE [dbo].[comentarios]
ADD CONSTRAINT [fk_comentarios_post]
    FOREIGN KEY ([id_post])
    REFERENCES [dbo].[publicaciones]
        ([id_post])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_comentarios_post'
CREATE INDEX [IX_fk_comentarios_post]
ON [dbo].[comentarios]
    ([id_post]);
GO

-- Creating foreign key on [id_user] in table 'comentarios'
ALTER TABLE [dbo].[comentarios]
ADD CONSTRAINT [FK_comentarios_users]
    FOREIGN KEY ([id_user])
    REFERENCES [dbo].[users]
        ([id_user])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_comentarios_users'
CREATE INDEX [IX_FK_comentarios_users]
ON [dbo].[comentarios]
    ([id_user]);
GO

-- Creating foreign key on [id_comentario] in table 'like_comentario'
ALTER TABLE [dbo].[like_comentario]
ADD CONSTRAINT [fk_like_comentario]
    FOREIGN KEY ([id_comentario])
    REFERENCES [dbo].[comentarios]
        ([id_comentario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_like_comentario'
CREATE INDEX [IX_fk_like_comentario]
ON [dbo].[like_comentario]
    ([id_comentario]);
GO

-- Creating foreign key on [id_post] in table 'interaccion_post'
ALTER TABLE [dbo].[interaccion_post]
ADD CONSTRAINT [FK_interaccion_post_publicaciones]
    FOREIGN KEY ([id_post])
    REFERENCES [dbo].[publicaciones]
        ([id_post])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_interaccion_post_publicaciones'
CREATE INDEX [IX_FK_interaccion_post_publicaciones]
ON [dbo].[interaccion_post]
    ([id_post]);
GO

-- Creating foreign key on [id_perfilvisitado] in table 'interaccion_post'
ALTER TABLE [dbo].[interaccion_post]
ADD CONSTRAINT [FK_interaccion_post_users]
    FOREIGN KEY ([id_perfilvisitado])
    REFERENCES [dbo].[users]
        ([id_user])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_interaccion_post_users'
CREATE INDEX [IX_FK_interaccion_post_users]
ON [dbo].[interaccion_post]
    ([id_perfilvisitado]);
GO

-- Creating foreign key on [id_userqueinteractuo] in table 'interaccion_post'
ALTER TABLE [dbo].[interaccion_post]
ADD CONSTRAINT [FK_interaccion_post_users1]
    FOREIGN KEY ([id_userqueinteractuo])
    REFERENCES [dbo].[users]
        ([id_user])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_interaccion_post_users1'
CREATE INDEX [IX_FK_interaccion_post_users1]
ON [dbo].[interaccion_post]
    ([id_userqueinteractuo]);
GO

-- Creating foreign key on [id_user] in table 'like_comentario'
ALTER TABLE [dbo].[like_comentario]
ADD CONSTRAINT [fk_like_comentario_users]
    FOREIGN KEY ([id_user])
    REFERENCES [dbo].[users]
        ([id_user])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_like_comentario_users'
CREATE INDEX [IX_fk_like_comentario_users]
ON [dbo].[like_comentario]
    ([id_user]);
GO

-- Creating foreign key on [id_post] in table 'like_post'
ALTER TABLE [dbo].[like_post]
ADD CONSTRAINT [FK_like_post]
    FOREIGN KEY ([id_post])
    REFERENCES [dbo].[publicaciones]
        ([id_post])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_like_post'
CREATE INDEX [IX_FK_like_post]
ON [dbo].[like_post]
    ([id_post]);
GO

-- Creating foreign key on [id_user] in table 'like_post'
ALTER TABLE [dbo].[like_post]
ADD CONSTRAINT [FK_Like_user]
    FOREIGN KEY ([id_user])
    REFERENCES [dbo].[users]
        ([id_user])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Like_user'
CREATE INDEX [IX_FK_Like_user]
ON [dbo].[like_post]
    ([id_user]);
GO

-- Creating foreign key on [id_post] in table 'publicacion_guardada'
ALTER TABLE [dbo].[publicacion_guardada]
ADD CONSTRAINT [FK_publicacion_guardada_publicaciones]
    FOREIGN KEY ([id_post])
    REFERENCES [dbo].[publicaciones]
        ([id_post])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_publicacion_guardada_publicaciones'
CREATE INDEX [IX_FK_publicacion_guardada_publicaciones]
ON [dbo].[publicacion_guardada]
    ([id_post]);
GO

-- Creating foreign key on [id_user] in table 'publicacion_guardada'
ALTER TABLE [dbo].[publicacion_guardada]
ADD CONSTRAINT [FK_publicacion_guardada_users]
    FOREIGN KEY ([id_user])
    REFERENCES [dbo].[users]
        ([id_user])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_publicacion_guardada_users'
CREATE INDEX [IX_FK_publicacion_guardada_users]
ON [dbo].[publicacion_guardada]
    ([id_user]);
GO

-- Creating foreign key on [id_user] in table 'publicaciones'
ALTER TABLE [dbo].[publicaciones]
ADD CONSTRAINT [FK_post_user_creator]
    FOREIGN KEY ([id_user])
    REFERENCES [dbo].[users]
        ([id_user])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_post_user_creator'
CREATE INDEX [IX_FK_post_user_creator]
ON [dbo].[publicaciones]
    ([id_user]);
GO

-- Creating foreign key on [id_post] in table 'visita_pefil_post'
ALTER TABLE [dbo].[visita_pefil_post]
ADD CONSTRAINT [FK_visita_pefil_post_publicaciones]
    FOREIGN KEY ([id_post])
    REFERENCES [dbo].[publicaciones]
        ([id_post])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_visita_pefil_post_publicaciones'
CREATE INDEX [IX_FK_visita_pefil_post_publicaciones]
ON [dbo].[visita_pefil_post]
    ([id_post]);
GO

-- Creating foreign key on [seguidor_id] in table 'seguidores'
ALTER TABLE [dbo].[seguidores]
ADD CONSTRAINT [fk_seguidores_user]
    FOREIGN KEY ([seguidor_id])
    REFERENCES [dbo].[users]
        ([id_user])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_seguidores_user'
CREATE INDEX [IX_fk_seguidores_user]
ON [dbo].[seguidores]
    ([seguidor_id]);
GO

-- Creating foreign key on [id_seguido] in table 'seguidores'
ALTER TABLE [dbo].[seguidores]
ADD CONSTRAINT [FK_seguidores_users]
    FOREIGN KEY ([id_seguido])
    REFERENCES [dbo].[users]
        ([id_user])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_seguidores_users'
CREATE INDEX [IX_FK_seguidores_users]
ON [dbo].[seguidores]
    ([id_seguido]);
GO

-- Creating foreign key on [id_usuario] in table 'usuario_categoria'
ALTER TABLE [dbo].[usuario_categoria]
ADD CONSTRAINT [FK__usuario_c__id_us__5EBF139D]
    FOREIGN KEY ([id_usuario])
    REFERENCES [dbo].[users]
        ([id_user])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__usuario_c__id_us__5EBF139D'
CREATE INDEX [IX_FK__usuario_c__id_us__5EBF139D]
ON [dbo].[usuario_categoria]
    ([id_usuario]);
GO

-- Creating foreign key on [id_perfilvisitado] in table 'visita_busqueda'
ALTER TABLE [dbo].[visita_busqueda]
ADD CONSTRAINT [FK_visita_busqueda_users]
    FOREIGN KEY ([id_perfilvisitado])
    REFERENCES [dbo].[users]
        ([id_user])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_visita_busqueda_users'
CREATE INDEX [IX_FK_visita_busqueda_users]
ON [dbo].[visita_busqueda]
    ([id_perfilvisitado]);
GO

-- Creating foreign key on [id_userquebusco] in table 'visita_busqueda'
ALTER TABLE [dbo].[visita_busqueda]
ADD CONSTRAINT [FK_visita_busqueda_users1]
    FOREIGN KEY ([id_userquebusco])
    REFERENCES [dbo].[users]
        ([id_user])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_visita_busqueda_users1'
CREATE INDEX [IX_FK_visita_busqueda_users1]
ON [dbo].[visita_busqueda]
    ([id_userquebusco]);
GO

-- Creating foreign key on [id_perfilvisitado] in table 'visita_pefil_post'
ALTER TABLE [dbo].[visita_pefil_post]
ADD CONSTRAINT [FK_visita_pefil_post_users]
    FOREIGN KEY ([id_perfilvisitado])
    REFERENCES [dbo].[users]
        ([id_user])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_visita_pefil_post_users'
CREATE INDEX [IX_FK_visita_pefil_post_users]
ON [dbo].[visita_pefil_post]
    ([id_perfilvisitado]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------