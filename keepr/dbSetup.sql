CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture',
        coverImg varchar(255) COMMENT 'User Cover Image'
    ) default charset utf8 COMMENT '';

ALTER TABLE accounts ADD COLUMN coverImg VARCHAR(255);

-- hell yeah

-- SECTION Keeps

CREATE TABLE
    IF NOT EXISTS keeps(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        creatorId VARCHAR(255) NOT NULL,
        name VARCHAR(55) NOT NULL,
        description TEXT NOT NULL,
        img TEXT NOT NULL,
        views INT DEFAULT 0,
        kept INT,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) DEFAULT CHARSET UTF8 COMMENT '';

-- createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,

-- updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,

DROP TABLE keeps;

CREATE TABLE
    IF NOT EXISTS vaults(
        id INT NOT NULL AUTO_INCREMENT primary key,
        creatorId VARCHAR(255) NOT NULL,
        name TEXT NOT NULL,
        description TEXT NOT NULL,
        img TEXT NOT NULL,
        isPrivate TINYINT DEFAULT 0,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8;

DROP TABLE vaults;

CREATE TABLE
    IF NOT EXISTS vaultkeeps(
        id INT NOT NULL AUTO_INCREMENT primary key,
        creatorId VARCHAR(255) NOT NULL,
        vaultId INT NOT NULL,
        keepId INT NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
        FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
        FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
    ) default charset utf8;

DROP TABLE vaultkeeps 