import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HiveService } from '../services/hive.service';
import { Hive } from '../models/hive';

@Component({
  selector: 'app-hive-form',
  templateUrl: './hive-form.component.html',
  styleUrls: ['./hive-form.component.css']
})

export class HiveFormComponent implements OnInit {

  hive = new Hive(0, "", "", "", false, "");
  existed = false;
  alertType: string;
  alertMessage: string;
  hasAlert: boolean;

  private successAlertType = "success";
  private errorAlertType = "danger";

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private hiveService: HiveService
  ) { }

  ngOnInit() {
    this.route.params.subscribe(p => {
      if (p['id'] === undefined) return;
      this.hiveService.getHive(p['id']).subscribe(h => this.hive = h);
      this.existed = true;
    });
  }

  navigateToHives() {
    this.router.navigate(['/hives']);
  }

  onCancel() {
    this.navigateToHives();
  }
  
  onSubmit() {
    if (this.existed) {
      this.hiveService.updateHive(this.hive).subscribe(
        r => this.showAlert(this.successAlertType, "Hive successfully saved."), 
        error => this.showAlert(this.errorAlertType, error));
    }
    else {
      this.hiveService.addHive(this.hive).subscribe(
        h => {
          this.hive = h;
          this.existed = true;
          this.showAlert(this.successAlertType, "Hive successfully created.");
        }, 
        error => this.showAlert(this.errorAlertType, error));
    }
  }

  onDelete() {
    this.hiveService.setHiveStatus(this.hive.id, true).subscribe(
      r => {
        this.hive.isDeleted = true;
        this.showAlert(this.successAlertType, "Hive marked as delete.");
      },
      error => this.showAlert(this.errorAlertType, error));
  }

  onUndelete() {
    this.hiveService.setHiveStatus(this.hive.id, false).subscribe(
      r => {
        this.hive.isDeleted = false;
        this.showAlert(this.successAlertType, "Hive marked as undelete.");
      },
      error => this.showAlert(this.errorAlertType, error));
  }

  onPurge() {
    this.hiveService.deleteHive(this.hive.id).subscribe(
      r => this.navigateToHives(),
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
