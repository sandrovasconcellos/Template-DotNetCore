"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserDataService = void 0;
var UserDataService = /** @class */ (function () {
    //httpclient possui os comando para se comunicar com o banco
    function UserDataService(http) {
        this.http = http;
        //variavel com o modulo, ou seja, o endereço da api
        this.module = 'api/users';
    }
    //metodo
    UserDataService.prototype.get = function () {
        return this.http.get(this.module);
    };
    return UserDataService;
}());
exports.UserDataService = UserDataService;
//# sourceMappingURL=user.data-services.js.map