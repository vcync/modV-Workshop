export default {
  meta: {
    type: '2d',
    name: 'Circle',
    author: 'NERDDISCO'
  },

  props: {
    size: {
      type: 'int',
      default: 20,
      min: 0,
      max: 100,
      abs: true,
    },

    color: {
      type: 'color',
      default: 'red',
    },
  },

  draw({ canvas: { width, height }, context, props }) {
    const { color, size } = props;

    context.fillStyle = color;
    context.beginPath();
    context.arc(
      (width / 2),
      (height / 2),
      size,
      0,
      Math.PI * 2,
    );
    context.fill();
  },
};