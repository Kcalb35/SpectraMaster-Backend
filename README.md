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

## MassProblems

| column  | type  | remarks     |
| ------- | ----- | ----------- |
| Id      | INT   | primary key |
| IonPeak | FLOAT |             |

## AnswerPics / ProblemPics

| column   | type | remarks     |
| -------- | ---- | ----------- |
| Id       | INT  | primary key |
| Path     | text |             |
| AnswerId | INT  | foreign key |

## NMRProblemAnswer

| column       | type | remarks          |
| ------------ | ---- | ---------------- |
| NMRProblemId | INT  | together primay  |
| AnswerId     | INT  | together primary |

## MassProblemAnswer

| column        | type | remarks          |
| ------------- | ---- | ---------------- |
| MassProblemId | INT  | together primary |
| AnswerId      | INT  | together primary |

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

delete the whole answer

- we need the id of `SpectraAnswer`

update

the same as create

retrieve

- if NMR, we need the number of atoms and whether to show no molecular formula
- if Mass , we need ion peak (or range)
- if complex , we need both