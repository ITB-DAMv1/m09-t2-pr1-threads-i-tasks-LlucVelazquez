# Read.me

## Solució proposada

La solució implementa una versió simplificada del joc **Asteroids** en consola, utilitzant la classe `Task` per aconseguir una programació paral·lela i asíncrona. L’objectiu principal és garantir una execució fluida del joc, amb actualització de posicions dels asteroides i la nau, repintat de la pantalla i gestió d’entrada de teclat de manera simultània, tot protegint les dades compartides.

### Estratègies i decisions d’implementació

- **Paral·lelisme i asincronia:**
S’han creat tres tasques principals: una per calcular les posicions (50 Hz), una per repintar la consola (20 Hz) i una per escoltar l’entrada de teclat en tot moment. Això permet que cada funcionalitat treballi independentment, millorant la resposta del joc i evitant bloquejos.
- **Sincronització:**
Per evitar condicions de cursa i garantir la coherència de les dades (posicions de la nau i asteroides), s’ha utilitzat un objecte `lock`. Aquest mecanisme assegura que només una tasca pot modificar o llegir les dades compartides en cada instant.
- **Gestió de la mida de la consola:**
S’utilitzen les propietats `Console.WindowWidth`, `Console.WindowHeight`, `Console.BufferWidth` i `Console.BufferHeight` per garantir que la nau i els asteroides es mantinguin dins dels límits visibles. A més, el joc força la mida de la finestra i el buffer per evitar que l’usuari pugui redimensionar la consola manualment.
- **Simulació de càrrega paral·lela:**
S’ha afegit una tasca addicional que simula una avaluació de webs, amb una durada aleatòria de 30 a 60 segons, per demostrar la capacitat de gestionar càrrega paral·lela sense afectar la jugabilitat.


### Esquema de l’estructura del projecte

```
AsteroidsGame/
│
├── Program.cs         // Codi principal del joc
├── Read.me           // Documentació i explicació de la solució
```

- **Program.cs:** Conté la lògica del joc, la gestió de tasques, la sincronització i la interacció amb la consola.
- **Read.me:** Explicació de la solució, decisions tècniques i respostes als enunciats.

---
### Enunciat 1: Com has fet per evitar interbloquejos i que ningú passes fam.



### Enunciat 2: Com has executat les tasques per tal de pintar, calcular i escoltar el teclat al mateix temps? Has diferenciat entre programació paral·lela i asíncrona?

Les tres funcions principals del joc (pintar, calcular i escoltar el teclat) s’executen en **tasques independents** utilitzant la classe `Task`. Això permet que cada tasca s’executi de forma concurrent, aprofitant el paral·lelisme del sistema operatiu.

- **Pintar (render):** Tasca que s’executa cada 50 ms (20 Hz).
- **Calcular (update):** Tasca que s’executa cada 20 ms (50 Hz).
- **Escoltar teclat:** Tasca que espera activament l’entrada de l’usuari sense bloquejar la resta del joc.

**Diferència entre programació paral·lela i asíncrona:**

- **Paral·lela:** Les tasques poden executar-se realment alhora en diferents nuclis de la CPU.
- **Asíncrona:** Les tasques poden executar-se de manera no bloquejant, però no necessàriament alhora.

En aquest projecte, s’aprofiten ambdues: les tasques són asíncrones (no bloquegen el fil principal) i, si el sistema ho permet, poden executar-se en paral·lel.

---

## Respostes als enunciats
[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/xs3aclQL)
