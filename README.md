# 📝 Bloco de Notas com LocalStorage

Uma aplicação web simples de bloco de notas que permite ao usuário criar, editar e salvar anotações diretamente no navegador usando LocalStorage.

![HTML5](https://img.shields.io/badge/HTML5-E34F26?style=for-the-badge&logo=html5&logoColor=white)
![CSS3](https://img.shields.io/badge/CSS3-1572B6?style=for-the-badge&logo=css3&logoColor=white)
![JavaScript](https://img.shields.io/badge/JavaScript-F7DF1E?style=for-the-badge&logo=javascript&logoColor=black)

---

## 🎯 Sobre o Projeto

O objetivo deste projeto é demonstrar o uso da **Web Storage API (LocalStorage)** para criar persistência de dados no lado do cliente, sem a necessidade de um banco de dados ou back-end.

## ✨ Funcionalidades

* **Criação de Notas:** Um campo de texto (textarea) para escrever anotações.
* **Salvamento Automático:** (Opcional) A nota é salva conforme o usuário digita.
* **Persistência de Dados:** As notas permanecem salvas no `LocalStorage` do navegador, mesmo após fechar a aba ou o navegador.
* **Carregamento de Notas:** Ao reabrir a página, a última nota salva é carregada automaticamente.

## 💻 Tecnologias Utilizadas

* **HTML5:** Estrutura da página (textarea, botões).
* **CSS3:** Estilização da interface.
* **JavaScript (ES6+):** Lógica para salvar (`localStorage.setItem`) e carregar (`localStorage.getItem`) as notas.

## 🚀 Como Visualizar

1.  Clone o repositório.
2.  Abra o arquivo `index.html` em qualquer navegador web.
3.  Digite algo e recarregue a página para ver a persistência.

---
Desenvolvido por **Raul Rodrigues**
