create database AgendamentoPaciente;

create table SituacaoAgendamento(
Id int not null auto_increment primary key,
Situacao varchar(50) not null);

create table Convenio(
Id int not null auto_increment primary key,
Nome varchar(255) not null);

create table Clinica(
Id int not null auto_increment primary key,
Nome varchar(255) not null,
Cnpj varchar(15) not null,
Telefone varchar(15) not null);

create table Paciente(
Id int not null auto_increment primary key,
Nome varchar(255) not null,
Cpf varchar(15) not null,
Email varchar(100),
ConvenioId int,
NumeroConvenio int,
foreign key(ConvenioId) references Convenio(Id));

create table Endereco(
Id int not null auto_increment primary key,
Cep varchar(15) not null,
Logradouro varchar(255) not null,
Numero varchar(10),
Complemento varchar(255),
ClinicaId int,
PacienteId int,
foreign key(ClinicaId) references Clinica(Id),
foreign key(PacienteId) references Paciente(Id));

create table Agendamento(
Id int not null auto_increment primary key,
DataAgendamento datetime not null,
ClinicaId int not null,
PacienteId int not null,
SituacaoAgendamentoId int not null,
foreign key(ClinicaId) references Clinica(Id),
foreign key(PacienteId) references Paciente(Id),
foreign key(SituacaoAgendamentoId) references SituacaoAgendamento(Id));