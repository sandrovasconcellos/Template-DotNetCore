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
  user: any = {};
  userLogin: any = {};
  userLogged: any = {};
  showList: boolean = true;
  isAuthenticated: boolean = false;

  constructor( private userDataService: UserDataService) { }

  //chamada toda vez que é chamada
  ngOnInit() {
    
  }

  get() {
    //chama o metodo do UserDataService
    //subscribe função para esperar responsta, tornando o metodo assincrono
    //data - recebe o retorno do backend
    //error - recebe caso ocorra erro
    this.userDataService.get().subscribe((data: any []) => {
      this.users = data;
      this.showList = true;
    }, error => {
      console.log(error);
      alert('Erro interno do sistema');
    })
  }

  save() {
    if (this.user.id) {
      this.put();
    } else {
      this.post();
    }
  }

  openDetails(user) {
    this.showList = false;
    this.user = user;
  }

  post() {
    this.userDataService.post(this.user).subscribe(data => {
      if (data) {
        alert('Usuario cadastrado com sucesso!');
        this.get();
        this.user = {};
      } else {
        alert('Erro ao cadastrar usurio!');
      }
    }, error => {
      console.log(error);
      alert('Erro interno do sistema');
    });
  }

  put() {
    this.userDataService.put(this.user).subscribe(data => {
      if (data) {
        alert('Usuario alterado com sucesso!');
        this.get();
        this.user = {};
      } else {
        alert('Erro ao alterar usuario!');
      }
    }, error => {
      console.log(error);
      alert('Erro interno do sistema');
    });
  }

  delete() {
    this.userDataService.delete().subscribe(data => {
      if (data) {
        alert('Usuario excluir com sucesso!');
        this.get();
        this.user = {};
      } else {
        alert('Erro ao excluir usuario!');
      }
    }, error => {
      console.log(error);
      alert('Erro interno do sistema');
    });
  }

  authenticate() {
    //data é uma variavel do tipo any
    this.userDataService.authenticate(this.userLogin).subscribe((data:any) => {
      if (data.user) {
        localStorage.setItem('user_logged', JSON.stringify(data));
        this.get();
        this.getUserData();
      } else {
        alert('Usuário inválido.');
      }
    }, error => {
      console.log(error);
      alert('Usuário não encontrado.');
    });
  }

  getUserData() {
    this.userLogged = JSON.parse(localStorage.getItem('user_logged'));
    this.isAuthenticated = this.userLogged != null;
  }
  
}


