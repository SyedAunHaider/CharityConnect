import { Component, OnInit } from '@angular/core';
// Must import to use Forms functionality  
import { FormBuilder, FormGroup, Validators, FormsModule, NgForm } from '@angular/forms';


@Component({
  selector: 'app-home',
  templateUrl: './user-registration.component.html',
})
export class UserRegistrationComponent implements OnInit {

  regiForm: FormGroup;
  FullName: string;
  Cnic: string;
  Mobile: number;
  DonationType: string;
  IsDonor: boolean;
  FamilyMembers: number;
  Province: string;
  City: string;
  NationalAssembly: string;
  ProvincialAssembly: string;
 
  constructor(private fb: FormBuilder)
  {

    //Variable Initilization
    this.FullName = '';
    this.Cnic = '';
    this.Mobile = 0;
    this.DonationType = '';
    this.IsDonor = true;
    this.FamilyMembers = 0;
    this.Province = '';
    this.City = '';
    this.NationalAssembly = '';
    this.ProvincialAssembly = '';

    // To initialize FormGroup  
    this.regiForm = fb.group({
      'FullName': [null, Validators.required],
      'Cnic': [null, Validators.compose([Validators.required, Validators.pattern("^[0-9]{13}$"), Validators.minLength(13), Validators.maxLength(13)])],
      'Mobile': [null, Validators.compose([Validators.required, Validators.pattern("^[0-9]{10}$"), Validators.minLength(10), Validators.maxLength(10)])],
      'DonationType': [null, Validators.required],
      'FamilyMembers': [null, Validators.pattern("0*[0-9][0-9]*")],
      'Province': [null, Validators.required],
      'City': [null, Validators.required],
      'NationalAssembly': [null, Validators.required],
      'ProvincialAssembly': [null, Validators.required]
    });  
  }

  ngOnInit() {
  }

  typeOnChange(event: any) {
    debugger;
    if (event.value == "Donor") {
      this.IsDonor = true;
    } else {
      this.IsDonor = false;
    }
  }

  // Executed When Form Is Submitted  
  onFormSubmit(form: NgForm) {
    console.log(form);
  }  
}
