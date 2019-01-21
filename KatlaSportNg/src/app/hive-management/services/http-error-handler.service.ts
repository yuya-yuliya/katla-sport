import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HttpErrorHandlerService {
  constructor() { }

  handleError(messageMap: {[key: number]: string}) { //Map<number, string>) {
    return (error: HttpErrorResponse) => {
      console.error(error);

      let message: string;
      if ((error instanceof ErrorEvent) || messageMap == null || messageMap[error.status] == undefined) {
        message = "Something bad happened. Please try again later." 
          + (error instanceof ErrorEvent) ? "" : ` Code: ${error.status}.`;
      }
      else {
        message = messageMap[error.status];
      }

      return throwError(message);
    }
  }
}
