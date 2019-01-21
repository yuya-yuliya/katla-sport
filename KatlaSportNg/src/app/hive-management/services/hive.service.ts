import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Hive } from '../models/hive';
import { HiveListItem } from '../models/hive-list-item';
import { HiveSectionListItem } from '../models/hive-section-list-item';
import { HttpErrorHandlerService } from './http-error-handler.service';

@Injectable({
  providedIn: 'root'
})
export class HiveService {
  private url = environment.apiUrl + 'api/hives/';
  private hiveNotFound = "Hive not found.";
  private hiveBadRequest = "Invalid hive information.";
  private hiveConflict = "Hive with given Code exists.";
  private hiveSectionNotFound = "Hive section not found.";

  constructor(
    private http: HttpClient,
    private errorHandler: HttpErrorHandlerService
  ) { }

  getHives(): Observable<Array<HiveListItem>> {
    return this.http.get<Array<HiveListItem>>(this.url)
      .pipe(
        catchError(this.errorHandler.handleError({}))
      );
  }

  getHive(hiveId: number): Observable<Hive> {
    return this.http.get<Hive>(`${this.url}${hiveId}`)
      .pipe(
        catchError(this.errorHandler.handleError({}))
      );
  }

  getHiveSections(hiveId: number): Observable<Array<HiveSectionListItem>> {
    return this.http.get<Array<HiveSectionListItem>>(`${this.url}${hiveId}/sections`)
      .pipe(
        catchError(this.errorHandler.handleError({
          404: this.hiveSectionNotFound
        }))
      );
  }

  addHive(hive: Hive): Observable<Hive> {
    return this.http.post<Hive>(`${this.url}`, hive)
      .pipe(
        catchError(this.errorHandler.handleError({
          400: this.hiveBadRequest,
          409: this.hiveConflict
        }))
      );
  }

  updateHive(hive: Hive): Observable<Object> {
    return this.http.put<Hive>(`${this.url}${hive.id}`, hive)
      .pipe(
        catchError(this.errorHandler.handleError({
          400: this.hiveBadRequest,
          404: this.hiveNotFound,
          409: this.hiveConflict
        }))
      );
  }

  deleteHive(hiveId: number): Observable<Object> {
    return this.http.delete<Hive>(`${this.url}${hiveId}`)
      .pipe(
        catchError(this.errorHandler.handleError({
          404: this.hiveNotFound
        }))
      );
  }

  setHiveStatus(hiveId: number, deletedStatus: boolean): Observable<Object> {
    return this.http.put(`${this.url}${hiveId}/status/${deletedStatus}`, null)
      .pipe(
        catchError(this.errorHandler.handleError({
          404: this.hiveNotFound
        }))
      );
  }
}
