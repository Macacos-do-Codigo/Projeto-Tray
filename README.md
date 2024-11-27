# **Eplace**
### Uma solução simplificada para gerenciar múltiplos ecommerces em um hub unificado, facilitando a integração com várias plataformas.

---

## **Passo-a-passo para configuração do projeto**

### **1. Requisitos prévios**
- Instalar o [Visual Studio](https://visualstudio.microsoft.com/) 
- Verificar se possui a versão 8.0 ou superior do .NET Core SDK
- Instalar [Apache](https://www.apachefriends.org/pt_br/index.html)
- Instalar [Node.js](https://nodejs.org/en) e [NPM](https://www.npmjs.com/)

### **2. Configuração do ambiente**
 **Clonar o projeto** ou baixar o código para o seu ambiente local:
   
   ```bash
   git clone <link-do-repositório>
   ```

### **Abrir o projeto no Visual Studio**
- Navegue até o arquivo `Eplace.sln` e abra-o com o Visual Studio.

### **3. Configuração da base de dados**
- No terminal do Visual Studio, executar o comando para atualizar o banco de dados:
  
   ```bash
   cd infrastructure
   ```
   ```bash
   dotnet ef database update --startup-project ../presentation --project .
   ```
   
### **4. Executar o projeto**
. Verifique no Visual Studio se o projeto de inicialização é 'Presentention'.
. Rodar o projeto no Visual Studio (pressionando **F5** ou clicando em "Run").
 **Iniciar o servidor Apache**:
   - Abra o Apache e assegure-se de que ele está em execução.
   - No browser, vá até `localhost`.
 **Acessar o Swagger** para visualizar a documentação da API:
   - No navegador, digite `http://localhost:<porta>/swagger` (substitua `<porta>` pela porta correta).

### **5. Instalar dependências do cliente**
. Navegar até a pasta do projeto `E-Place` no terminal (CMD ou terminal do Visual Studio):

   ```bash
   cd E-Place
   ```

### Instalar as dependências necessárias, como o `axios`:

  ```bash
  npm install axios
  ```

### Rodar o ambiente de desenvolvimento do frontend:

  ```bash
  npm run dev
  ```

## **Integrantes**

| Nome                   | Número de RA |
|------------------------|---------------------|
| **Matheus Gabriel e Silva** | 1967134             |
| **Silvio de Moraes Neto**   | 1969365             |
| **Renan Solano**            | 1968758             |
| **Vinicius Medeiros**       | 1970157             |
| **Henrique Medeiros**       | 1976730             |
