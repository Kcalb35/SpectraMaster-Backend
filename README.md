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



## MassProblemAnswer



## Admins

not implement