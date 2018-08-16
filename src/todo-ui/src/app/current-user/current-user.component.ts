import { Component, OnInit } from '@angular/core';
import { OAuthService} from 'angular-oauth2-oidc';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-current-user',
  templateUrl: './current-user.component.html',
  styleUrls: ['./current-user.component.css']
})
export class CurrentUserComponent implements OnInit {

  public user = '';
  constructor(private oauthService: OAuthService,
              private router: Router) { }

  ngOnInit() {
    this.user = this.name;
  }
  public get name() {
    const claims: any = this.oauthService.getIdentityClaims();
    console.log(claims);
    if (!claims) {
      return null;
    }
    return claims.name;
  }

  public navigateToImport() {
      this.router.navigate(['/import']);
  }
}
