import { Component, OnInit } from '@angular/core';
import { OAuthService} from 'angular-oauth2-oidc';

@Component({
  selector: 'app-current-user',
  templateUrl: './current-user.component.html',
  styleUrls: ['./current-user.component.css']
})
export class CurrentUserComponent implements OnInit {

  public user = '';
  constructor(private oauthService: OAuthService) { }

  ngOnInit() {
    this.user = this.name;
  }
  public get name() {
    const claims = this.oauthService.getIdentityClaims();
    console.log(claims);
    if (!claims) {
      return null;
    }
    return claims.full_name;
  }
}
