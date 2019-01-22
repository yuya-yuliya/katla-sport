import { Component, OnInit } from '@angular/core';
import { HiveSection } from '../models/hive-section';
import { ActivatedRoute, Router } from '@angular/router';
import { HiveSectionService } from '../services/hive-section.service';
import { HiveService } from '../services/hive.service';
import { HiveListItem } from '../models/hive-list-item';

@Component({
  selector: 'app-hive-section-form',
  templateUrl: './hive-section-form.component.html',
  styleUrls: ['./hive-section-form.component.css']
})
export class HiveSectionFormComponent implements OnInit {

  hiveSection = new HiveSection(0, "", "", 0, false, "");
  hives: HiveListItem[];

  existed = false;
  alertType: string;
  alertMessage: string;
  hasAlert: boolean;

  private successAlertType = "success";
  private errorAlertType = "danger";

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private hiveSectionService: HiveSectionService,
    private hiveService: HiveService
  ) { }

  ngOnInit() {
    this.hiveService.getHives().subscribe(
      h => this.hives = h,
      error => this.showAlert(this.errorAlertType, error));

    this.route.params.subscribe(p => {
        if (p['id'] == undefined) {
          if (p['hiveId'] != undefined) {
            this.hiveSection.storeHiveId = p['hiveId'];
          }
          return;
        }
        this.hiveSectionService.getHiveSection(p['id']).subscribe(s => {
          this.hiveSection = s;
          this.existed = true;
      },
      error => this.showAlert(this.errorAlertType, error));
    });
  }

  navigateToSections() {
    this.router.navigate([`hive/${this.hiveSection.storeHiveId}/sections`]);
  }

  onCancel() {
    this.navigateToSections();
  }
  
  onSubmit() {
    if (this.existed) {
      this.hiveSectionService.updateHiveSection(this.hiveSection).subscribe(
        r => this.showAlert(this.successAlertType, "Hive Section successfully saved."), 
        error => this.showAlert(this.errorAlertType, error));
    }
    else {
      this.hiveSectionService.addHiveSection(this.hiveSection).subscribe(
        s => {
          this.router.navigate([`/section/${s.id}`]);
        }, 
        error => this.showAlert(this.errorAlertType, error));
    }
  }

  onDelete() {
    this.hiveSectionService.setHiveSectionStatus(this.hiveSection.id, true).subscribe(
      r => {
        this.hiveSection.isDeleted = true;
        this.showAlert(this.successAlertType, "Hive Section marked as delete.");
      },
      error => this.showAlert(this.errorAlertType, error));
  }

  onUndelete() {
    this.hiveSectionService.setHiveSectionStatus(this.hiveSection.id, false).subscribe(
      r => {
        this.hiveSection.isDeleted = false;
        this.showAlert(this.successAlertType, "Hive Section marked as undelete.");
      },
      error => this.showAlert(this.errorAlertType, error));
  }

  onPurge() {
    this.hiveSectionService.deleteHiveSection(this.hiveSection.id).subscribe(
      r => this.navigateToSections(),
      error => this.showAlert(this.errorAlertType, error)
    );
  }

  alertClose() {
    this.hasAlert = false;
  }

  private showAlert(type: string, message: string) {
    this.hasAlert = true;
    this.alertType = type;
    this.alertMessage = message;
  }
}
