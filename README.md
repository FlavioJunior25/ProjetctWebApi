# Project

Projeto WebApi

Descrição: Esse projeto e responsavel por essas atividades:

Criar um serviço REST (Web API) contenha 2 métodos expostos.
</br>
*AddItemFila
</br>
O objetivo do método AddItemFila é adicionar um objeto json com o formato abaixo em uma
</br>
fila de processamento (utilizar o objeto que desejar para o armazenamento).
</br>
**Criar as validações de entrada que achar necessário
</br>
Formato objeto Json de entrada:
</br>
[
</br>
 {
 </br>
 "moeda": "USD",
 </br>
 "data_inicio": "2010-01-01",
 </br>
 "data_fim": "2010-12-01"
 </br>
 },
 </br>
 {
 </br>
 "moeda": "EUR",
 </br>
 "data_inicio": "2020-01-01",
 </br>
 "data_fim": "2010-12-01"
 </br>
 },
 </br>
 {
 </br>
 "moeda": "JPY",
 </br>
 "data_inicio": "2000-03-11",
 </br>
 "data_fim": "2000-03-30"
 </br>
 }
 </br>
]
</br>
* GetItemFila
</br>
O objetivo do método GetItemFila é retornar o último objeto json adicionado na fila pelo
método AddItemFila.
</br>
o Caso exista o objeto a ser retornado, retorná-lo e retirá-lo da fila.
</br>
o Caso não exista, retornar algo sinalize que não existe objeto a ser retornado.
