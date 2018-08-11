import { Component, OnInit } from '@angular/core';
import { OAuthService, JwksValidationHandler} from "angular-oauth2-oidc";

@Component({
  selector: 'app-current-user',
  templateUrl: './current-user.component.html',
  styleUrls: ['./current-user.component.css']
})
export class CurrentUserComponent implements OnInit {

  public user: string = '';
  constructor(private oauthService: OAuthService) { }

  ngOnInit() {
    this.user = this.name;
  }
  public get name() {
    let claims = this.oauthService.getIdentityClaims();
    let profile = this.oauthService.loadUserProfile().then(res => console.log(res));
    console.log(claims);
    if (!claims)
      return null;
    return claims.full_name;
  }
}
