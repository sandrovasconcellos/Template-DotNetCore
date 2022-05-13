import { Component, OnInit } from '@angular/core';
import { UserDataService } from '../_data-services/user.data-services';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  //se comunica com o user.dataservices

  //array de um objeto não definido, sendo inicialida como vazio
  users: any[] = [];

  constructor( private userDataService: UserDataService) { }

  //chamada toda vez que é chamada
  ngOnInit() {
    this.get();
  }

  get() {
    //chama o metodo do UserDataService
    //subscribe função para esperar responsta, tornando o metodo assincrono
    //data - recebe o retorno do backend
    //error - recebe caso ocorra erro
    this.userDataService.get().subscribe((data: any []) => {
      this.users = data;
    }, error => {
      console.log(error);
      alert('erro interno do sistema');
    })

  }

}


