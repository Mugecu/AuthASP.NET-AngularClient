import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './components/home/home.component';
import { FavoritesComponent } from './components/favorites/favorites.component';
import { AUTH_API_URL, BOOKCLUB_API_URL } from './app-injection-tokens';
import { environment } from 'src/environments/environment';
import {JwtModule} from '@auth0/angular-jwt';
import { ACCESS_TOKEN_KEY } from './services/auth.service';

export function tokenGetter(){
  return localStorage.getItem(ACCESS_TOKEN_KEY);
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    FavoritesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule

    JwtModule.forRoot({
      config:{
        tokenGetter,        
        allowedDomains: environment.tokenWhiteListedDomains
      }
    })
  ],
  providers: [{
    provide: AUTH_API_URL,
    useValue:environment.authApi
  },
  {
    provide: BOOKCLUB_API_URL,
    useValue: environment.bookclubApi
  }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
