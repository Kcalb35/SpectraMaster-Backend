# Spectra Master

A server for NMR spectra problem and Mass spectra problem collecting and searching.

# Database

## SpectraAnswers

| column name        | type     | remarks |
| ------------------ | -------- | ------- |
| Id                 | int      | primary |
| ProblemDescription | longtext |         |
| AnswerDescription  | longtext |         |

## NMRProblems

| column name | type     | remarks         |
| ----------- | -------- | --------------- |
| Id          | INT      | primary         |
| H           | SMALLINT | number of atoms |
| C           | SMALLINT |                 |
| N           | SMALLINT |                 |
| O           | SMALLINT |                 |
| F           | SMALLINT |                 |
| Si          | SMALLINT |                 |
| P           | SMALLINT |                 |
| S           | SMALLINT |                 |
| Cl          | SMALLINT |                 |
| Br          | SMALLINT |                 |
| I           | SMALLINT |                 |
| AnswerId    | INT      | foreign key     |

## MassProblems

| column   | type  | remarks     |
| -------- | ----- | ----------- |
| Id       | INT   | primary key |
| IonPeak  | FLOAT |             |
| AnswerId | INT   | foreign key |

## AnswerPics / ProblemPics

| column   | type | remarks     |
| -------- | ---- | ----------- |
| Id       | INT  | primary key |
| Path     | text |             |
| AnswerId | INT  | foreign key |

## Admins

not implement

# CRUD

create

create answer with descriptions ,list of pictures in format of string

- if NMR , we need number of atoms (or all-zero for no molecular formula)
- if Mass, we need ion peak
- if comlex , we need both
- return a `SpectraAnswer`，and put not null `ProblemAnswer` member to Dbset

delete

- delete the whole answer

- delete nmr problem
- delete mass problem

update

the same as create

retrieve

- retrieve answer massProblem NMRProblem by id
- retrieve all answer/massProblem/NMRProblem
- retrieve answer by nmr or mass property



# Backend API

- Get By id
  -  `api/ans/id` GET
  - return everything
- get by mass id
  -  `api/mass/id` GET
  - return mass id ,problem pictures, problem description, ion peak, formula =null
- get by nmr id 
  - `api/nmr/id` GET
  - return nmr id, problem pictures, problem description, formula, ion peak = -1
- get all answer `api/ans` GET
- get all mass `api/mass` GET
- get all nmr `api/nmr` GET
- delete By id `api/ans/id` DELETE
- delte by nmr id `api/nmr/id` DELETE
- delete by mass id `api/mass/id` DELETE
- update By id with information `api/ans/id` PUT
- Search  `api/ans/search` POST
  - if nmr ,then `minionpeak=maxionpeak=-1`
  - if mass, then `formula=null`