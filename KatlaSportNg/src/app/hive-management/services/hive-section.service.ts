import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { HiveSection } from '../models/hive-section';
import { HiveSectionListItem } from '../models/hive-section-list-item';
import { HttpErrorHandlerService } from './http-error-handler.service';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class HiveSectionService {
  private url = environment.apiUrl + 'api/sections/';
  private hiveSectionNotFound = "Hive section not found.";
  private hiveSectionConflict = "Hive section information has conflict Code.";
  private hiveSectionBadRequest = "Invalid hive section information.";
  private hiveSectionDeleteConflict = "Hive section must have 'Deleted' status.";

  constructor(
    private http: HttpClient,
    private errorHandler: HttpErrorHandlerService
  ) { }

  getHiveSections(): Observable<Array<HiveSectionListItem>> {
    return this.http.get<Array<HiveSectionListItem>>(this.url)
      .pipe(
        catchError(this.errorHandler.handleError({}))
      );
  }

  getHiveSection(hiveSectionId: number): Observable<HiveSection> {
    return this.http.get<HiveSection>(`${this.url}${hiveSectionId}`)
      .pipe(
        catchError(this.errorHandler.handleError({
          404: this.hiveSectionNotFound
        }))
      );
  }

  setHiveSectionStatus(hiveSectionId: number, deletedStatus: boolean): Observable<Object> {
    return this.http.put(`${this.url}${hiveSectionId}/status/${deletedStatus}`, null)
      .pipe(
        catchError(this.errorHandler.handleError({
          404: this.hiveSectionNotFound
        }))
      );
  }

  addHiveSection(hiveSection: HiveSection): Observable<HiveSection> {
    return this.http.post<HiveSection>(`${this.url}`, hiveSection)
      .pipe(
        catchError(this.errorHandler.handleError({
          400: this.hiveSectionBadRequest,
          409: this.hiveSectionConflict
        }))
      );
  }

  updateHiveSection(hiveSection: HiveSection): Observable<Object> {
    return this.http.put<HiveSection>(`${this.url}${hiveSection.id}`, hiveSection)
      .pipe(
        catchError(this.errorHandler.handleError({
          400: this.hiveSectionBadRequest,
          404: this.hiveSectionNotFound,
          409: this.hiveSectionConflict
        }))
      );
  }

  deleteHiveSection(hiveSectionId: number): Observable<Object> {
    return this.http.delete(`${this.url}${hiveSectionId}`)
      .pipe(
        catchError(this.errorHandler.handleError({
          404: this.hiveSectionNotFound,
          409: this.hiveSectionDeleteConflict
        }))
      );
  }
}
