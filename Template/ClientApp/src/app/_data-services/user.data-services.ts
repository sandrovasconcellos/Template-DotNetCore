import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable()

export class UserDataService {

  //variavel com o modulo, ou seja, o endereço da api
  module: string = 'api/users';

  //httpclient possui os comando para se comunicar com o banco
  constructor(private http: HttpClient) {
  }

  //metodos de comunicação com a API

  get() {
    return this.http.get(this.module);
  }

  //o metodo post necessita de body
  post(data) {
    return this.http.post(this.module, data);
  }

  put(data) {
    return this.http.put(this.module, data);
  }

  delete(userId) {
    return this.http.delete(this.module + '/' + userId);
  }

  authenticate(data) {
    return this.http.post(this.module + '/authenticate', data);
  }

}
