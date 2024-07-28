create database if not exists contatos;
use contatos;
create table if not exists contato (
    id int auto_increment primary key,
    nome varchar(255) not null,
    telefone varchar(15) not null,
    email varchar(255) not null
);