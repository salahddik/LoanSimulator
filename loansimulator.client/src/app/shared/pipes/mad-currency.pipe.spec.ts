import { MadCurrencyPipe } from './mad-currency.pipe';

describe('MadCurrencyPipe', () => {
  let pipe: MadCurrencyPipe;

  beforeEach(() => {
    pipe = new MadCurrencyPipe();
  });

  it('should create an instance', () => {
    expect(pipe).toBeTruthy();
  });

  describe('transform()', () => {
    it('should format number as MAD currency', () => {
      const result = pipe.transform(1234.56);
      // More flexible pattern to handle different locale formats
      expect(result).toMatch(/1[,.\s]?234[,.]56\s?MAD/);
    });

    it('should handle zero value', () => {
      const result = pipe.transform(0);
      expect(result).toMatch(/0[,.]00\s?MAD/);
    });

    it('should handle large numbers', () => {
      const result = pipe.transform(1234567.89);
      expect(result).toMatch(/1[.,\s]?234[.,\s]?567[,.]89\s?MAD/);
    });

    it('should return empty string for non-number input', () => {
      expect(pipe.transform('not a number' as any)).toBe('');
      expect(pipe.transform(null)).toBe('');
      expect(pipe.transform(undefined)).toBe('');
      expect(pipe.transform({})).toBe('');
    });

    it('should format negative numbers correctly', () => {
      const result = pipe.transform(-1234.56);
      expect(result).toMatch(/-1[.,\s]?234[,.]56\s?MAD/);
    });

    it('should handle decimal numbers', () => {
      const result = pipe.transform(1234.5);
      expect(result).toMatch(/1[.,\s]?234[,.]50\s?MAD/);
    });
  });
});
