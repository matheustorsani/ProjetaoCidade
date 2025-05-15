create database dbProjetaoCidadeFromBahia;
use dbProjetaoCidadeFromBahia;

create table tbUsuario (
	id int not null auto_increment primary key,
    nome varchar(200) not null,
    email varchar(200) not null, 
    senha varchar(30) not null
);

create table tbProdutos (
	id int not null auto_increment primary key,
    nome varchar(200) not null,
    descricao varchar(400),
    preco decimal(8,2) not null,
    quantidade int not null
);

