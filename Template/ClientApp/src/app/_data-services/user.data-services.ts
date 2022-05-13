import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable()

export class UserDataService {

  //variavel com o modulo, ou seja, o endereço da api
  module: string = 'api/users';

  //httpclient possui os comando para se comunicar com o banco
  constructor(private http: HttpClient) {
  }

  //metodo
  get() {
    return this.http.get(this.module);
  }
}
