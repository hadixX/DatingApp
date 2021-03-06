import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getUsers(page?: any, itemsPerPage?: any): Observable<PaginatedResult<User[]>> {
    const paginatedResult: PaginatedResult<User[]> = new PaginatedResult<User[]>();
    
    let params = new HttpParams();

    if(page != null && itemsPerPage != null){
      params.append('pageNumber',page);
      params.append('pageSize',itemsPerPage);
    }
    console.log(params.toString());
    return this.http.get<User[]>(this.baseUrl + 'user',{ observe: 'response', params }).pipe(
      map(response =>{
        paginatedResult.result = response.body;
        if(response.headers.get('Pagination') != null){
          paginatedResult.pagination = JSON.parse(response.headers.get('pagination'))
        }
        return paginatedResult;
      })
    );
  }

  getUser(id: number): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'user/' + id);
  }

  updateUser(id: number,user: User){
    return this.http.put(this.baseUrl+'user/'+id,user);
  }

  setMainPhoto(userId: number,id:number){
    return this.http.post(this.baseUrl+'users/'+userId+'/photos/'+id+'/setMain',{});
  }

  deletePhoto(userId:number, id:number){
    return this.http.delete(this.baseUrl+'users/'+userId+'/photos/'+id);
  }

}
