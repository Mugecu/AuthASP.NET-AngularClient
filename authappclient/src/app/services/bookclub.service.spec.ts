import { TestBed } from '@angular/core/testing';

import { BookclubService } from './bookclub.service';

describe('BookclubService', () => {
  let service: BookclubService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BookclubService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
