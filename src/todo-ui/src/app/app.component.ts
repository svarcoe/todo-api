import { Component } from '@angular/core';
import { OAuthService, JwksValidationHandler} from 'angular-oauth2-oidc';
import { authConfig } from './app.authconfig';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private oauthService: OAuthService) {
    this.oauthService.configure(authConfig);
    this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    this.oauthService.loadDiscoveryDocumentAndTryLogin();
  }

  title = 'app';

  ngOnInit() {
    this.oauthService.loadDiscoveryDocumentAndTryLogin().then(_ => {
      if (
          !this.oauthService.hasValidIdToken() ||
          !this.oauthService.hasValidAccessToken()
      ) {
          this.oauthService.initImplicitFlow('some-state');
      }
      this.oauthService.loadUserProfile().then(res => console.log(res));
  });
  }
}
