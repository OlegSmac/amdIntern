import { useState } from 'react';
import { FaChevronLeft, FaChevronRight } from 'react-icons/fa';

export type Image = {
  url: string;
};

export type ImageGalleryProps = {
  images: Image[];
};

const ImageGallery = ({ images }: ImageGalleryProps) => {
  const [currentIndex, setCurrentIndex] = useState(0);
  const mainImage = images[currentIndex]?.url;

  const handlePrevImage = () => {
    setCurrentIndex((prev) => (prev === 0 ? images.length - 1 : prev - 1));
  };

  const handleNextImage = () => {
    setCurrentIndex((prev) => (prev === images.length - 1 ? 0 : prev + 1));
  };

  return (
    <>
      <div style={{ position: 'relative', display: 'flex', justifyContent: 'center' }}>
        <button
          onClick={handlePrevImage}
          style={{
            position: 'absolute',
            left: '-50px',
            top: '50%',
            zIndex: 2,
            background: 'transparent',
            border: 'none',
            cursor: 'pointer',
            outline: 'none',
            transform: 'translateY(-50%)',
          }}
          aria-label="Previous Image"
        >
          <FaChevronLeft size={22} />
        </button>

        {mainImage && (
          <img
            src={mainImage}
            alt={`Main image ${currentIndex + 1}`}
            style={{
              width: '650px',
              height: '400px',
              objectFit: 'cover',
              borderRadius: '8px',
            }}
          />
        )}

        <button
          onClick={handleNextImage}
          style={{
            position: 'absolute',
            right: '-50px',
            top: '50%',
            zIndex: 2,
            background: 'transparent',
            border: 'none',
            cursor: 'pointer',
            outline: 'none',
            transform: 'translateY(-50%)',
          }}
          aria-label="Next Image"
        >
          <FaChevronRight size={22} />
        </button>
      </div>

      <div style={{ display: 'flex', marginTop: '5px', gap: '10px', flexWrap: 'wrap' }}>
        {images.map((img, index) => (
          <img
            key={index}
            src={img.url}
            alt={`Thumbnail ${index + 1}`}
            onClick={() => setCurrentIndex(index)}
            style={{
              width: '120px',
              height: '80px',
              marginTop: '20px',
              objectFit: 'cover',
              cursor: 'pointer',
              border: index === currentIndex ? '2px solid blue' : '1px solid gray',
              borderRadius: '4px',
            }}
          />
        ))}
      </div>
    </>
  );
};

export default ImageGallery;
