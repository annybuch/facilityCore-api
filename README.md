**FacilityCore**
FacilityCore é uma biblioteca em andamento desenvolvida para facilitar a interação com bancos de dados MongoDB e MySQL, além de fornecer suporte para autenticação JWT. Projetada para uso em aplicações ASP.NET Core, ela oferece uma série de serviços que simplificam operações CRUD, manipulação de entidades e autenticação de usuários.

**Recursos**
A biblioteca inclui serviços abrangentes para MongoDB, permitindo inserções, atualizações, exclusões e consultas de documentos de forma síncrona e assíncrona. Além disso, há suporte para pesquisas avançadas utilizando a distância de Levenshtein para encontrar correspondências aproximadas em nomes, tornando as buscas mais flexíveis e eficazes.


Para MySQL, FacilityCore API oferece métodos para realizar operações CRUD e incluir entidades relacionadas em consultas, além de verificar a existência de registros. A pesquisa por nome também é suportada, utilizando a função LIKE do SQL em combinação com a distância de Levenshtein para aprimorar a precisão dos resultados.

**Autenticação JWT**
A biblioteca fornece funcionalidades para geração e validação de tokens JWT, essenciais para a autenticação de usuários em APIs. Inclui middleware para validar tokens presentes nas requisições e serviços para manipulação e extração de informações do token JWT, facilitando a gestão de usuários autenticados.

FacilityCore ainda está em constante desenvolvimento, com a intenção de adicionar mais recursos e aprimorar a integração com diferentes bancos de dados e sistemas de autenticação.
